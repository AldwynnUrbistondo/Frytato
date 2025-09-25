using UnityEngine;

public class Knife : MonoBehaviour
{
    Rigidbody rb;
    DragAndDrop dragAndDrop;

    private bool hasSliced = false; // prevent multiple slices in one swing
    [SerializeField] private float sliceCooldown = 0.1f; // adjust as needed
    private float lastSliceTime = -999f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dragAndDrop = GetComponent<DragAndDrop>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SliceablePotato sliceable = other.GetComponentInParent<SliceablePotato>();

        if (sliceable != null)
        {
            // Require dragging, downward motion, and cooldown passed
            if (dragAndDrop.isDragging && rb.linearVelocity.y < -0.05f && Time.time - lastSliceTime > sliceCooldown)
            {
                sliceable.SlicePotato();
                lastSliceTime = Time.time;
                hasSliced = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // reset when knife fully leaves the potato
        if (other.GetComponentInParent<SliceablePotato>() != null)
        {
            hasSliced = false;
        }
    }
}
