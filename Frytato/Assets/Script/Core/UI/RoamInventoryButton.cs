
using UnityEngine;
using UnityEngine.UI;

public class RoamInventoryButton: InventoryButton
{

    public override void OnClick()
    {

        UIManager.Instance.roamUI.EquipSelect(itemData);

    }
}
