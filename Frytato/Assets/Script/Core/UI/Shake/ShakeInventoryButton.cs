using UnityEngine;

public class ShakeInventoryButton : InventoryButton
{

    public override void OnClick()
    {
        if (ShakeManager.Instance.CanAddFries())
        {
            Instantiate(itemData.itemObject, UIManager.Instance.shakeUI.spawnPoint.position, Quaternion.identity);
            InventoryManager.Instance.RemoveItem(itemData, 1);
            ShakeManager.Instance.AddFries();
        }
    }
}
