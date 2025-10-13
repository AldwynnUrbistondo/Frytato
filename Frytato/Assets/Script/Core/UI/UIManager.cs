using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Screens")]
    public RoamUI roamUI;
    public SliceUI sliceUI;
    public FryUI fryUI;
    public ShakeUI shakeUI;
    public OrderUI orderUI;

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
        // Update all UIs
        roamUI.UpdateInventoryUI();
        sliceUI.UpdateInventoryUI();
        fryUI.UpdateInventoryUI();
        shakeUI.UpdateInventoryUI();

        // Switch by game state
        switch (GameManager.Instance.gameState)
        {
            case GameState.Roam:
                roamUI.roamUICanvas.SetActive(true);

                sliceUI.sliceUICanvas.SetActive(false);

                fryUI.fryUICanvas.SetActive(false);
                fryUI.UnSelect();

                shakeUI.shakeUICanvas.SetActive(false);

                orderUI.orderUICanvas.SetActive(false);
                break;

            case GameState.Slice:
                roamUI.roamUICanvas.SetActive(false);
                sliceUI.sliceUICanvas.SetActive(true);
                break;

            case GameState.Fry:
                roamUI.roamUICanvas.SetActive(false);
                fryUI.fryUICanvas.SetActive(true);
                break;

            case GameState.Shake:
                roamUI.roamUICanvas.SetActive(false);
                shakeUI.shakeUICanvas.SetActive(true);
                break;

            case GameState.Order:
                roamUI.roamUICanvas.SetActive(false);
                orderUI.orderUICanvas.SetActive(true);
                break;

            default:
                break;
        }
    }
}
