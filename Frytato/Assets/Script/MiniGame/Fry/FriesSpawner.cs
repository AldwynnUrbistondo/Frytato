using UnityEngine;

public class FriesSpawner : MonoBehaviour
{
    public FryerClick fryer;


    private void OnMouseDown()
    {
        if (FryUI.Instance.selectedRawFries != null && !fryer.isCooking && fryer.currentFries < fryer.capacity && fryer.canAddFries)
        {
            GameObject rawFries = FryUI.Instance.selectedRawFries.itemObject;
            Instantiate(rawFries, transform.position, Quaternion.identity);
            InventoryManager.Instance.RemoveItem(FryUI.Instance.selectedRawFries);
            fryer.currentFries++;
        }
        
    }
}
