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
        interactable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        interactable = null;
    }


}
