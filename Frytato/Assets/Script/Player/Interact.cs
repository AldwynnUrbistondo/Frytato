using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] float radius = 0.5f;      // Radius of the sphere
    [SerializeField] float distance = 3f;      // Max distance
    [SerializeField] LayerMask interactableMask;

    private RaycastHit hit;
    private IInteractable currentTarget;       // the object we're currently aiming at

    void Update()
    {
        // --- Cast a "fat ray" ---
        bool hasHit = Physics.SphereCast(transform.position, radius, transform.forward, out hit, distance, interactableMask);

        if (hasHit)
        {
            var interactable = hit.collider.GetComponent<IInteractable>();

            // ENTER
            if (interactable != null && interactable != currentTarget)
            {
                // We were pointing at something new
                currentTarget?.OnRaycastExit();   // call exit on old
                currentTarget = interactable;
                currentTarget.OnRaycastEnter();
            }

            // STAY
            if (interactable == currentTarget)
            {
                currentTarget.OnRaycastStay();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentTarget.Interact();
                }
            }
        }
        else
        {
            // EXIT if we had something last frame but now nothing
            if (currentTarget != null)
            {
                currentTarget.OnRaycastExit();
                currentTarget = null;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward * distance, radius);
    }
}
