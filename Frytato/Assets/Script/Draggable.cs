using UnityEngine;

public class Draggable : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float pullForce = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Drag(Vector3 targetPos)
    {
        rb.linearVelocity = (targetPos - rb.position) * pullForce;
    }

    public void Drop()
    {
        rb.linearVelocity = Vector3.zero;
    }

}
