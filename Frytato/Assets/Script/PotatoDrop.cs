using UnityEngine;

public class PotatoDrop : MonoBehaviour, ICollectible
{
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float raycastDistance = 0.5f;
    [SerializeField] LayerMask groundLayer;

    Rigidbody rb;
    bool isCollecting;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;          // we’ll apply gravity ourselves
        rb.isKinematic = false;
    }

    void Start()
    {
        float xForce = Random.Range(-1f, 1f);
        float zForce = Random.Range(-1f, 1f);

        // instant jump impulse
        rb.AddForce(new Vector3(xForce, 5f, zForce), ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        if (!isCollecting)
        {
            if (IsGrounded())
            {
                rb.linearVelocity = Vector3.zero;
                isCollecting = true;
            }
            else
            {
                rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
            }

        }
        else
        {
            GoToTargetPos();
        }

    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);
    }

    void GoToTargetPos()
    {
        Vector3 targetPos = GameObject.FindWithTag("Player").transform.position;
        rb.linearVelocity = (targetPos - transform.position).normalized * 10f;
    }

    public void Collect()
    {
        // pick-up logic
    }
}
