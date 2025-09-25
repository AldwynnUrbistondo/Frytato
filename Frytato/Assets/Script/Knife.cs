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

    private void OnTriggerEnter(Collider other)
    {
        SliceablePotato sliceable = other.GetComponentInParent<SliceablePotato>();

        Debug.Log(rb.linearVelocity.y);

        if (sliceable != null)
        {
            
            if (dragAndDrop.isDragging && rb.linearVelocity.y < -1f)
            {
                sliceable.SlicePotato();
                dragAndDrop.Release();
            }

        }
        

    }
}
