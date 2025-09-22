using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Scroll View Inventory")]
    public GameObject scrollViewInventoryPanel;
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
        #region Scroll View Inventory
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
        #endregion
    }

}
