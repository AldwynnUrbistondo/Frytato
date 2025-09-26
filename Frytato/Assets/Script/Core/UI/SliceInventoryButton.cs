using UnityEngine;

public class SliceInventoryButton : InventoryButton
{

    public override void OnClick()
    {
        GameObject potato = Instantiate(itemData.itemObject, SliceUI.Instance.spawnPoint.position, Quaternion.identity);
        SliceablePotato sliceable = potato.GetComponent<SliceablePotato>();
        sliceable.potatoObject = (PotatoObject)itemData;
        InventoryManager.Instance.RemoveItem(itemData, 1);
    }
}
