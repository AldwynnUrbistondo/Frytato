using UnityEngine;
using System.Collections;

public class ShakeJar : MonoBehaviour
{
    [Header("Shake Settings")]
    public float moveAmount = 0.3f;
    public float moveSpeed = 8f;
    public float returnSpeed = 5f;
    public int shakesNeeded = 6;
    public Vector3 rotationFix = new Vector3(-90f, 0f, 0f);
    public bool canShake = false;
    public bool finishedShaking = false;

    [Header("Drag Settings")]
    [SerializeField] private float dragReturnSpeed = 5f;

    public Rigidbody rb;
    public DragAndDrop dragScript;

    [Header("Camera Settings")]
    public Camera shakeCamera;

    private Vector3 startPos;
    private Quaternion originalRotation;
    private bool isDragging = false;
    private float lastMouseY;
    public int shakeCount = 0;
    private bool hasReachedTop = false;
    private bool hasReachedBottom = true;
    private Coroutine returnRoutine;

    Animator anim;
    void Start()
    {
        startPos = transform.position;
        originalRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        dragScript = GetComponent<DragAndDrop>();
        anim = GetComponent<Animator>();

        anim.enabled = false;
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (dragScript != null)
        {
            dragScript.enabled = false;
        }
    }

    void Update()
    {
        if (shakeCamera != null && !shakeCamera.gameObject.activeInHierarchy)
            return;

        // Handle finished shaking - return to origin logic
        if (finishedShaking)
        {
            HandleDragReturn();
            return; // Exit early, no more shake logic
        }

        // Enable shake if fries are ready
        if (ShakeManager.Instance.friesinJarCount == 10 && ShakeManager.Instance.hasFlavor)
            canShake = true;

        // Keep jar upright during shake mode
        transform.rotation = Quaternion.Euler(rotationFix);

        if (!canShake)
        {
            // Return smoothly to resting position
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * returnSpeed);
            return;
        }

        HandleInput();

        if (shakeCount >= shakesNeeded)
        {
            Debug.Log("Finished shaking!");

            // TELEPORT to start position immediately
            transform.position = startPos;
            transform.rotation = originalRotation;

            canShake = false;
            finishedShaking = true;
            isDragging = false; // Stop any shake dragging
            shakeCount = 0;
            ShakeManager.Instance.friesinJarCount = 0;

            // Enable rigidbody and drag script
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            if (dragScript != null)
            {
                dragScript.enabled = true;
            }

            Debug.Log("Jar is now draggable!");
        }
    }

    void HandleInput()
    {
        if (!canShake) return;

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMouseY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float deltaY = (Input.mousePosition.y - lastMouseY) / Screen.height * moveSpeed;
            lastMouseY = Input.mousePosition.y;
            Vector3 pos = transform.position;
            pos.y += deltaY;
            pos.y = Mathf.Clamp(pos.y, startPos.y, startPos.y + moveAmount);
            transform.position = pos;

            if (pos.y >= startPos.y + moveAmount * 0.95f && !hasReachedTop)
            {
                hasReachedTop = true;
                hasReachedBottom = false;
            }
            else if (pos.y <= startPos.y + moveAmount * 0.05f && hasReachedTop && !hasReachedBottom)
            {
                hasReachedBottom = true;
                hasReachedTop = false;
                shakeCount++;
                Debug.Log($"Shake Count: {shakeCount}/{shakesNeeded}");
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * returnSpeed);
        }
    }

    void HandleDragReturn()
    {
        if (dragScript == null) return;

        if (dragScript.isDragging)
        {
            // Stop the return coroutine while dragging
            if (returnRoutine != null)
            {
                StopCoroutine(returnRoutine);
                returnRoutine = null;
            }
        }
        else
        {
            // If not dragging and not yet returning, move back to original position
            if (returnRoutine == null)
            {
                returnRoutine = StartCoroutine(ReturnToOrigin());
            }
        }
    }

    IEnumerator ReturnToOrigin()
    {
        float t = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        if (rb != null)
        {
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        while (t < 1f)
        {
            t += Time.deltaTime * dragReturnSpeed;
            transform.position = Vector3.Lerp(startPosition, startPos, t);
            transform.rotation = Quaternion.Slerp(startRotation, originalRotation, t);
            yield return null;
        }

        transform.position = startPos;
        transform.rotation = originalRotation;

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
        }

        returnRoutine = null;
    }

    public void SetCanShake(bool value)
    {
        canShake = value;
        if (!value)
        {
            isDragging = false;
            transform.position = startPos;
        }
    }

    public void ResetJar()
    {
        // Call this after fries are delivered
        finishedShaking = false;
        canShake = false;
        isDragging = false;
        shakeCount = 0;
        hasReachedTop = false;
        hasReachedBottom = true;

        // Stop any return coroutine
        if (returnRoutine != null)
        {
            StopCoroutine(returnRoutine);
            returnRoutine = null;
        }

        // Disable rigidbody
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Disable drag script
        if (dragScript != null)
        {
            dragScript.enabled = false;
        }

        // Reset position and rotation to original
        transform.position = startPos;
        transform.rotation = originalRotation;

        Debug.Log("Jar reset to shake mode");
    }
}