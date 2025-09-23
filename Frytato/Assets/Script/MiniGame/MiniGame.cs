using UnityEngine;

public class MiniGame : MonoBehaviour, IInteractable
{
    public GameObject canvas;
    public GameObject outlineObject;

    public GameState miniGameType;
    public CameraType miniGameCamera;

    public virtual void Interact()
    {
        GameManager.Instance.gameState = miniGameType;
        CameraManager.Instance.ChangeCamera(miniGameCamera);
        UIManager.Instance.UpdateUI();
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
