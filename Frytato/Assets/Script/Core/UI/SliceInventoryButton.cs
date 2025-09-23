using UnityEngine;

public class SliceInventoryButton : InventoryButton
{
    Transform spawnPoint;

    public override void OnClick()
    {
        spawnPoint = GameObject.FindWithTag("Slice Spawnpoint").transform;
        Instantiate(itemData.itemObject, spawnPoint.position, Quaternion.identity);
    }
}
