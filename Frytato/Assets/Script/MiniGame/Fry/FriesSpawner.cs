using UnityEngine;

public class FriesSpawner : MonoBehaviour
{
    public FryerClick fryer;

    private void OnMouseDown()
    {
        if (UIManager.Instance.fryUI.selectedRawFries != null && !fryer.isCooking && fryer.currentFries < fryer.capacity && fryer.canAddFries)
        {
            GameObject rawFries = UIManager.Instance.fryUI.selectedRawFries.itemObject;
            GameObject friesObject = Instantiate(rawFries, transform.position, Quaternion.identity);
            friesObject.transform.SetParent(fryer.transform, true);
            
            fryer.AddFriesToBasket(UIManager.Instance.fryUI.selectedRawFries, friesObject);
            InventoryManager.Instance.RemoveItem(UIManager.Instance.fryUI.selectedRawFries);
            fryer.currentFries++;
        }
        
    }
}
