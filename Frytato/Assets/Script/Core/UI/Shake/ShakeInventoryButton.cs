using UnityEngine;

public class ShakeInventoryButton : InventoryButton
{
    public override void OnClick()
    {
        Instantiate(itemData.itemObject);
    }
}
