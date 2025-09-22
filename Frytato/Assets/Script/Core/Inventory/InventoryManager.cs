using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField]
    public List<InventoryItem> items = new List<InventoryItem>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(ItemData itemData, int quantity = 1)
    {
        // Always merge with existing item if found
        foreach (var item in items)
        {
            if (item.itemData == itemData)
            {
                item.quantity += quantity;
                UIManager.Instance.UpdateUI();
                return;
            }
        }

        // Otherwise add new item
        items.Add(new InventoryItem(itemData, quantity));
        UIManager.Instance.UpdateUI();

    }

    public void RemoveItem(ItemData itemData, int quantity = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemData == itemData)
            {
                items[i].quantity -= quantity;
                if (items[i].quantity <= 0)
                {
                    items.RemoveAt(i);
                }  
                return;
            }
        }
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int quantity;

    public InventoryItem(ItemData data, int quantity = 1)
    {
        this.itemData = data;
        this.quantity = quantity;
    }
}



