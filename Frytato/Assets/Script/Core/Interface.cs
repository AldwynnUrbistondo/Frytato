using UnityEngine;

public interface IInteractable
{
    public void Interact();

    public void OnRaycastEnter();

    public void OnRaycastStay();

    public void OnRaycastExit();
}
