using UnityEngine;
using UnityEngine.UI;

public class EquipItem : MonoBehaviour
{
    public static EquipItem Instance { get; private set; }
    public Button equipButton;

    public ItemData equippedItem;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void EquipSelect(ItemData itemToEquip)
    {
        equippedItem = itemToEquip;
        equipButton.image.sprite = itemToEquip.itemIcon;
    }
}
