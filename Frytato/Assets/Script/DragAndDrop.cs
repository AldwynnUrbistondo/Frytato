using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Rigidbody rb; // assign manually in inspector
    [SerializeField] private float followSpeed = 10f; // how fast it follows the mouse
    [SerializeField] private LayerMask draggableMask; // assign draggable layer(s) in inspector

    [Header("Default Rotation")]
    [SerializeField] bool setDraggingRotation = false;
    [SerializeField] Vector3 defaultRotation = Vector3.zero;

    private Camera mainCamera;
    public bool isDragging = false;
    private Vector3 offset;
    private float zCoord;
    private float zLock;

    private RigidbodyConstraints originalConstraints; // store constraints before dragging

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        // Save original constraints so we can restore them later
        originalConstraints = rb.constraints;
    }

    void Update()
    {
        mainCamera = CameraManager.Instance.activeCamera;

        // Pick up
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, draggableMask) && hit.rigidbody == rb)
            {
                if (setDraggingRotation)
                {
                    rb.transform.eulerAngles = defaultRotation;
                }

                isDragging = true;
                rb.useGravity = false;

                // Freeze rotation
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                zCoord = mainCamera.WorldToScreenPoint(rb.position).z;
                zLock = rb.position.z;
                offset = rb.position - GetMouseWorldPos();
            }
        }

        // Release
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            rb.useGravity = true;
            rb.linearVelocity = Vector3.zero;

            rb.constraints = originalConstraints;
        }
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;
            targetPos.z = zLock; // lock Z

            Vector3 moveDir = (targetPos - rb.position);

            rb.linearVelocity = moveDir * followSpeed;
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
