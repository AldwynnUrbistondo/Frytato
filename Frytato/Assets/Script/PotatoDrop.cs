using System.Collections;
using UnityEngine;

public class PotatoDrop : MonoBehaviour, ICollectible
{
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float raycastDistance = 0.5f;
    [SerializeField] LayerMask groundLayer;
    Rigidbody rb;
    Collider col;
    bool isGrounded = false;
    bool isReadyToCollect = false;
    bool isCollecting = false;
    [SerializeField] PotatoObject potatoObject;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;

        col = GetComponent<Collider>();
        col.enabled = false;
    }

    void Start()
    {
        float xForce = Random.Range(-1f, 1f);
        float zForce = Random.Range(-1f, 1f);
        rb.AddForce(new Vector3(xForce, 5f, zForce), ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        // Apply gravity until grounded
        if (!isGrounded)
        {
            if (CheckGrounded())
            {
                isGrounded = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                StartCoroutine(CollectReady(0.1f)); // Small delay after landing
            }
            else
            {
                rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
            }
        }
        // Move toward player once ready to collect
        else if (isReadyToCollect)
        {
            GoToTargetPos();
        }
    }

    bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);
    }

    void GoToTargetPos()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 targetPos = player.transform.position;
            rb.linearVelocity = (targetPos - transform.position).normalized * 10f;
        }
    }

    public void Collect()
    {
        InventoryManager.Instance.AddItem(potatoObject, 1);
        AudioManager.Instance.PlaySound(SoundType.Collect);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isReadyToCollect && !isCollecting)
        {
            isCollecting = true;
            Collect();
            Destroy(gameObject);
        }
    }

    IEnumerator CollectReady(float delay)
    {
        yield return new WaitForSeconds(delay);
        isReadyToCollect = true;
        col.enabled = true;
    }
}