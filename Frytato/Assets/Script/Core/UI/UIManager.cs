using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Transform spawnLocation;
    public Button buttonPrefab;
    public List<InventoryItem> items = new List<InventoryItem>();

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
        foreach (Transform child in spawnLocation)
        {
            Destroy(child.gameObject);
        }
        items = InventoryManager.Instance.items;
        
    }

}
