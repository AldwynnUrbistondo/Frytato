using UnityEngine;

public class SliceInventoryButton : InventoryButton
{

    public override void OnClick()
    {
        InventoryItem inventoryItem = InventoryManager.Instance.items.Find(item => item.itemData == itemData);

        if (inventoryItem == null || inventoryItem.quantity == 1)
        {
            return; // Don't spawn if no items available
        }

        GameObject potato = Instantiate(itemData.itemObject, UIManager.Instance.sliceUI.spawnPoint.position, Quaternion.identity);
        SliceablePotato sliceable = potato.GetComponent<SliceablePotato>();
        sliceable.potatoObject = (PotatoObject)itemData;
        InventoryManager.Instance.RemoveItem(itemData, 1);
        AudioManager.Instance.PlaySound(SoundType.Collect);
    }
}
