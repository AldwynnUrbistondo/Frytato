using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    IInteractable interactable;
    [SerializeField] Button interactButton;

    private void Awake()
    {
        interactButton.onClick.AddListener(InteractButton);
        interactButton.interactable = false;
    }

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
            interactButton.interactable = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Only clear if the object exiting is the one we were interacting with
        var found = other.GetComponent<IInteractable>();
        if (found != null && found == interactable)
        {
            interactable = null;
            interactButton.interactable = false;
        }
    }

    void InteractButton()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }

}
