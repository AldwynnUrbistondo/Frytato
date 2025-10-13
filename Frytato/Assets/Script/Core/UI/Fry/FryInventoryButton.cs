using UnityEngine;

public class FryInventoryButton : InventoryButton
{
    public override void OnClick()
    {
        if (itemData is RawFriesObject)
        {
            UIManager.Instance.fryUI.EquipSelect(itemData as RawFriesObject);
        }
    }
}
