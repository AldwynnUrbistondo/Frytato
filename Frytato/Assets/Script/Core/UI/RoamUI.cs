using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoamUI : MonoBehaviour
{
    public static RoamUI Instance;

    [Header("Equip Button")]
    public Button equipButton;
    public ItemData equippedItem;

    [Header("Inventory Panel")]
    public GameObject scrollViewInventoryPanel;
    public Transform spawnLocation;
    public Button buttonPrefab;

    private List<InventoryItem> items = new List<InventoryItem>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        equipButton.onClick.AddListener(ToggleInventory);
    }

    // --- Equip System ---
    public void EquipSelect(ItemData itemToEquip)
    {
        equippedItem = itemToEquip;
        equipButton.image.sprite = itemToEquip.itemIcon;
    }

    public void Unequip()
    {
        equippedItem = null;
        equipButton.image.sprite = null; // or fallback sprite
    }

    // --- Inventory UI ---
    private void ToggleInventory()
    {
        scrollViewInventoryPanel.SetActive(!scrollViewInventoryPanel.activeSelf);

    }

    public void UpdateInventoryUI()
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

            InventoryPotatoButton buttonScript = newButton.GetComponent<InventoryPotatoButton>();
            buttonScript.itemData = item.itemData;

            TextMeshProUGUI text = newButton.GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{item.quantity}x";
        }
    }
}
