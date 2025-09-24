using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliceUI : InventoryUI
{

    public static SliceUI Instance;

    public GameObject sliceUICanvas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public override void UpdateInventoryUI()
    {
        foreach (Transform child in spawnLocation)
        {
            Destroy(child.gameObject);
        }


        items = InventoryManager.Instance.items;

        foreach (var item in items)
        {
            if (item.itemData is PotatoObject)
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
