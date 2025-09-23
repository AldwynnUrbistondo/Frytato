using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory Panel")]
    public GameObject scrollViewInventoryPanel;
    public Transform spawnLocation;
    public Button buttonPrefab;

    public List<InventoryItem> items = new List<InventoryItem>();

    public virtual void UpdateInventoryUI()
    {
        foreach (Transform child in spawnLocation)
        {
            Destroy(child.gameObject);
        }


        items = InventoryManager.Instance.items;

        foreach (var item in items)
        {
            Button newButton = Instantiate(buttonPrefab, spawnLocation);
            newButton.image.sprite = item.itemData.itemIcon;

            InventoryButton buttonScript = newButton.GetComponent<InventoryButton>();
            buttonScript.itemData = item.itemData;

            TextMeshProUGUI text = newButton.GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{item.quantity}x";
        }
    }
}
