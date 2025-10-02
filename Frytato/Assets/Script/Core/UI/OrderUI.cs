using UnityEngine;

public class OrderUI : MonoBehaviour
{
    public GameObject orderUICanvas;
    public CustomerInteract customerInteract;
    public void OrderClick()
    {
        if (customerInteract != null)
        {
            customerInteract.TryFinishOrder();
        }
    }
}
