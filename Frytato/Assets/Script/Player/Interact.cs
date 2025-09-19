using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] float radius = 0.5f;   // Radius of the "circle"
    [SerializeField] float distance = 3f;   // How far the sphere goes
    [SerializeField] LayerMask interactableMask; // Optional: filter what to hit

    RaycastHit hit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Fire a "fat" ray (sphere)
            if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, distance, interactableMask))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward * distance, radius);
    }
}
