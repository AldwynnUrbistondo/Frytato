using UnityEngine;

public class FriesSpawner : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (FryUI.Instance.selectedRawFries != null)
        {
            GameObject rawFries = FryUI.Instance.selectedRawFries.itemObject;
            Instantiate(rawFries, transform.position, Quaternion.identity);
            InventoryManager.Instance.RemoveItem(FryUI.Instance.selectedRawFries);
        }
        
    }
}
