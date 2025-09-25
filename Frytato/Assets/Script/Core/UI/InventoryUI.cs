using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; // for .Contains()

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory Panel")]
    public GameObject scrollViewInventoryPanel;
    public Transform spawnLocation;
    public Button buttonPrefab;

    public List<InventoryItem> items = new List<InventoryItem>();

    [Header("Filter Settings")]
    public ItemType[] showOnlyTheseTypes;

    public virtual void UpdateInventoryUI()
    {
        // clear old UI
        foreach (Transform child in spawnLocation)
        {
            Destroy(child.gameObject);
        }

        items = InventoryManager.Instance.items;

        foreach (var item in items)
        {
            if (showOnlyTheseTypes.Contains(item.itemData.itemType))
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
}
