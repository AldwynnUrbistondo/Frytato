using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoamUI : InventoryUI
{
    public GameObject roamUICanvas;

    [Header("Equip Button")]
    public Button equipButton;
    public ItemData equippedItem;
    public Sprite defaultIcon;

    private void Awake()
    {
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
        equipButton.image.sprite = defaultIcon;
    }

    // --- Inventory UI ---
    private void ToggleInventory()
    {
        scrollViewInventoryPanel.SetActive(!scrollViewInventoryPanel.activeSelf);

    }

}
