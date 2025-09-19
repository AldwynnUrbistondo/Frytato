using UnityEngine;

public class Interact : MonoBehaviour
{
    IInteractable interactable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
        {
            interactable.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        interactable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        interactable = null;
    }


}
