using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private LayerMask draggableMask;

    [Header("Default Rotation")]
    [SerializeField] bool setDraggingRotation = false;
    [SerializeField] Vector3 defaultRotation = Vector3.zero;

    private Camera mainCamera;
    public bool isDragging = false;
    private Vector3 offset;
    private float zCoord;
    private float zLock;

    private RigidbodyConstraints originalConstraints;
    private Coroutine dragFlagCoroutine;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        originalConstraints = rb.constraints;
    }

    void Update()
    {
        mainCamera = CameraManager.Instance.activeCamera;

        // Pick up
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, draggableMask) && hit.rigidbody == rb)
            {
                // setup immediately
                rb.useGravity = false;

                if (setDraggingRotation)
                {
                    rb.transform.eulerAngles = defaultRotation;
                    transform.position = new Vector3(transform.position.x, transform.position.y, 100.63f);
                }

                rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                 RigidbodyConstraints.FreezeRotationX |
                                 RigidbodyConstraints.FreezeRotationY |
                                 RigidbodyConstraints.FreezeRotationZ;

                zCoord = mainCamera.WorldToScreenPoint(rb.position).z;
                zLock = rb.position.z;
                offset = rb.position - GetMouseWorldPos();

                // delay only the bool
                if (dragFlagCoroutine != null) StopCoroutine(dragFlagCoroutine);
                dragFlagCoroutine = StartCoroutine(SetDraggingFlagAfterDelay(0.2f));
            }
        }

        // Release
        if (Input.GetMouseButtonUp(0))
        {
            if (dragFlagCoroutine != null) StopCoroutine(dragFlagCoroutine);
            if (isDragging) Release();
        }
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;
            targetPos.z = zLock;

            Vector3 moveDir = (targetPos - rb.position);
            rb.linearVelocity = moveDir * followSpeed;
        }
    }

    IEnumerator SetDraggingFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Input.GetMouseButton(0)) // still holding
            isDragging = true;
    }

    public void Release()
    {
        isDragging = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.constraints = originalConstraints;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
