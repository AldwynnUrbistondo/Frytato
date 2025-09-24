using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Rigidbody rb; // assign manually in inspector
    [SerializeField] private float followSpeed = 10f; // how fast it follows the mouse
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private float zCoord;
    private float zLock;


    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        mainCamera = CameraManager.Instance.activeCamera;

        // Pick up
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.rigidbody == rb)
            {
                isDragging = true;
                rb.useGravity = false; // keep collisions but disable gravity while dragging

                zCoord = mainCamera.WorldToScreenPoint(rb.position).z;
                zLock = rb.position.z;
                offset = rb.position - GetMouseWorldPos();
            }
        }

        // Release
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            rb.useGravity = true; // restore gravity
            rb.linearVelocity = Vector3.zero; // stop leftover momentum
        }
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;
            targetPos.z = zLock; // lock Z

            // Calculate movement direction
            Vector3 moveDir = (targetPos - rb.position);

            // Apply velocity towards target
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
