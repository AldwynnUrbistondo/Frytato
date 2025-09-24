using UnityEngine;

public class Knife : MonoBehaviour
{
    Rigidbody rb;
    DragAndDrop dragAndDrop;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dragAndDrop = GetComponent<DragAndDrop>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        SliceablePotato sliceable = collision.gameObject.GetComponent<SliceablePotato>();
        if (sliceable != null && dragAndDrop.isDragging && rb.linearVelocity.magnitude > 1)
        {
            sliceable.SlicePotato();
        }
        Debug.Log(rb.linearVelocity.magnitude);
    }
}
