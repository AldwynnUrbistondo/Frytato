using UnityEngine;

public class ShakeInventoryButton : InventoryButton
{
    public Transform friesSpawn;
    public override void OnClick()
    {
        Instantiate(itemData.itemObject, friesSpawn.position, transform.rotation);
    }
}
