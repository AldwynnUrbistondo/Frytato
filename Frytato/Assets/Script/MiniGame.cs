using UnityEngine;

public class MiniGame : MonoBehaviour, IInteractable
{
    public GameObject canvas;
    public GameObject outlineObject;

    public virtual void Interact()
    {
        GameManager.Instance.gameState = GameState.MiniGame;
    }

    public void Start()
    {
        outlineObject.SetActive(false);
        canvas.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outlineObject.SetActive(true);
            canvas.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outlineObject.SetActive(false);
            canvas.SetActive(false);
        }
    }
}
