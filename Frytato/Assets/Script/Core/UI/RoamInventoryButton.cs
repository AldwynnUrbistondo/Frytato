
using UnityEngine;
using UnityEngine.UI;

public class RoamInventoryButton: InventoryButton
{

    public override void OnClick()
    {

        RoamUI.Instance.EquipSelect(itemData);

    }
}
