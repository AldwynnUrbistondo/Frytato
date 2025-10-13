using UnityEngine;
using UnityEngine.UI;

public class FryUI : InventoryUI
{
    public GameObject fryUICanvas;
    public RawFriesObject selectedRawFries;
    public Image selectedFriesImage;

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
