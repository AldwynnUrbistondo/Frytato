
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void UpdateUI()
    {
        RoamUI.Instance.UpdateInventoryUI();
        SliceUI.Instance.UpdateInventoryUI();

        switch (GameManager.Instance.gameState)
        {

            case GameState.Roam:
                RoamUI.Instance.roamUICanvas.SetActive(true);
                SliceUI.Instance.sliceUICanvas.SetActive(false);
                break;

            case GameState.Slice:
                RoamUI.Instance.roamUICanvas.SetActive(false);
                SliceUI.Instance.sliceUICanvas.SetActive(true);
                break;

            case GameState.Fry:
                RoamUI.Instance.roamUICanvas.SetActive(false);

                break;

            case GameState.Shake:
                // Future implementation
                break;

            default:
                break;
        }

    }

}
