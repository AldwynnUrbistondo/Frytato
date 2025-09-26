using UnityEngine;
using UnityEngine.UI;

public class FryUI : InventoryUI
{
    public static FryUI Instance;

    public GameObject fryUICanvas;
    public RawFriesObject selectedRawFries;
    public Image selectedFriesImage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void EquipSelect(RawFriesObject selectedFries)
    {
        selectedRawFries = selectedFries;
        selectedFriesImage.sprite = selectedFries.itemIcon;
    }

    public void UnSelect()
    {

        if (selectedFriesImage != null)
        {
            selectedFriesImage.sprite = null;
        }
        if (selectedRawFries != null)
        {
            selectedRawFries = null;
        }

    }
}
