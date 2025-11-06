using UnityEngine;

public class ShakeInventoryButton : InventoryButton
{
    public Transform Jar;
    public override void Start()
    {
        base.Start();
        Jar = GameObject.FindGameObjectWithTag("Jar").transform;
    }
    public override void OnClick()
    {
        // Only proceed if the shake manager allows fries to be added
        if (ShakeManager.Instance.CanAddFries())
        {
            // Spawn the fries object
            GameObject spawnedFries = Instantiate(
                itemData.itemObject,
                UIManager.Instance.shakeUI.spawnPoint.position,
                Quaternion.identity
            );
            spawnedFries.transform.SetParent(Jar, true);
            ShakeManager.Instance.AddFriesToJar(itemData as CookFriesObject, spawnedFries);
            if (itemData is CookFriesObject cookFries)
            {
                Renderer rend = spawnedFries.GetComponent<Renderer>();
                if (rend != null && cookFries.cookTexture != null)
                {
                    rend.material.mainTexture = cookFries.cookTexture;
                }
            }

            // Remove from inventory and add to ShakeManager
            InventoryManager.Instance.RemoveItem(itemData, 1);
            ShakeManager.Instance.AddFries();
        }
    }
}