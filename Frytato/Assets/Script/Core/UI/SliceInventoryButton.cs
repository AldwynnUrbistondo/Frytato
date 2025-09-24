using UnityEngine;

public class SliceInventoryButton : InventoryButton
{
    Transform spawnPoint;

    public override void OnClick()
    {
        spawnPoint = GameObject.FindWithTag("Slice Spawnpoint").transform;
        GameObject potato = Instantiate(itemData.itemObject, spawnPoint.position, Quaternion.identity);
        SliceablePotato sliceable = potato.GetComponent<SliceablePotato>();
        sliceable.potatoObject = (PotatoObject)itemData;
        InventoryManager.Instance.RemoveItem(itemData, 1);
    }
}
