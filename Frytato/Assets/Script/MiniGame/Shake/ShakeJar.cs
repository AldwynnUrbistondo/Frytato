using UnityEngine;

public class ShakeJar : MonoBehaviour
{
    [Header("Shake Settings")]
    public float moveAmount = 0.3f;      // How far the jar can move upward
    public float moveSpeed = 8f;         // How fast it follows the drag
    public float returnSpeed = 5f;       // How fast it returns when released
    public int shakesNeeded = 6;         // How many full shakes to complete
    public Vector3 rotationFix = new Vector3(-90f, 0f, 0f);
    public bool canShake = false;

    [Header("Camera Settings")]
    public Camera shakeCamera;


    private Vector3 startPos;
    private bool isDragging = false;
    private bool movingUp = false;
    private bool movingDown = false;
    private float lastMouseY;
    public int shakeCount = 0;
    private bool hasReachedTop = false;
    private bool hasReachedBottom = true; // starts at bottom

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (shakeCamera != null && !shakeCamera.gameObject.activeInHierarchy)
            return;
        // Enable shake if fries are ready

        if (ShakeManager.Instance.friesinJarCount == 10 && ShakeManager.Instance.hasFlavor)
            canShake = true;

        // Keep jar upright
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
            Debug.Log(" Finished shaking!");
            canShake = false;
            shakeCount = 0;
            ShakeManager.Instance.friesinJarCount = 0;
            transform.position = startPos;
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

            // Clamp between startPos.y (bottom) and moveAmount above
            pos.y = Mathf.Clamp(pos.y, startPos.y, startPos.y + moveAmount);
            transform.position = pos;

            // Detect shake movement (up then down counts as 1)
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
            // Return to original resting position when not dragging
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * returnSpeed);
        }
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
}