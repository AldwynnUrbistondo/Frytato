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
        if (sliceable != null)
        {
            Debug.Log(rb.linearVelocity.y);
            if (dragAndDrop.isDragging && rb.linearVelocity.y < 0.2f)
            {
                sliceable.SlicePotato();
                dragAndDrop.Release();
            }

        }
        
    }
}
