using NUnit.Framework;
using System.Collections;
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
            Canvas canvas = UIManager.Instance.shakeUI.shakeUICanvas.GetComponent<Canvas>();
            canvas.enabled = false;

            StartCoroutine(AddFries());
        }
    }

    IEnumerator AddFries()
    {

        for (int i = 0; i < 10; i++)
        {
            

            // Spawn the fries object
            GameObject spawnedFries = Instantiate(
                itemData.itemObject,
                UIManager.Instance.shakeUI.spawnPoint.position,
                Quaternion.identity
            );
            spawnedFries.transform.SetParent(Jar, true);
            CookFries friesData = spawnedFries.GetComponent<CookFries>();
            friesData.cookFriesObject = itemData as CookFriesObject;

            ShakeManager.Instance.AddFriesToJar(itemData as CookFriesObject, spawnedFries);
            if (itemData is CookFriesObject cookFries)
            {
                Renderer rend = spawnedFries.GetComponent<Renderer>();
                if (rend != null && cookFries.cookTexture != null)
                {
                    rend.material.mainTexture = cookFries.cookTexture;
                }
            }

            ShakeManager.Instance.AddFries();

            yield return new WaitForSeconds(0.1f);
        }

        // Remove from inventory and add to ShakeManager
        InventoryManager.Instance.RemoveItem(itemData, 10);

    }
}