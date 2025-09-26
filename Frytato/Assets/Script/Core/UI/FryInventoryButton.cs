using UnityEngine;

public class FryInventoryButton : InventoryButton
{
    public override void OnClick()
    {
        if (itemData is RawFriesObject)
        {
            FryUI.Instance.EquipSelect(itemData as RawFriesObject);
        }
    }
}
