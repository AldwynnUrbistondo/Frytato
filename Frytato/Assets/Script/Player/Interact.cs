using UnityEngine;

public class Interact : MonoBehaviour
{
    IInteractable interactable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
        {
            interactable.Interact();
            Debug.Log("Interacted with " + interactable.ToString());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var found = other.GetComponent<IInteractable>();
        if (found != null)
        {
            interactable = found;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Only clear if the object exiting is the one we were interacting with
        var found = other.GetComponent<IInteractable>();
        if (found != null && found == interactable)
        {
            interactable = null;
        }
    }


}
