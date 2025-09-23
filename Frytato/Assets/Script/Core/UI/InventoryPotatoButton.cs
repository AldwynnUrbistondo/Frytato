
using UnityEngine;
using UnityEngine.UI;

public class InventoryPotatoButton : MonoBehaviour
{
    Button button;
    public ItemData itemData;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (GameManager.Instance.gameState == GameState.Roam)
        {
            RoamUI.Instance.EquipSelect(itemData);
        }
    }

    public void SpawnPotato()
    {
        InventoryManager.Instance.RemoveItem(itemData, 1);
    }
}
