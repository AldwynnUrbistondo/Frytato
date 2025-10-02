using UnityEngine;

public class OrderUI : MonoBehaviour
{
    public CustomerInteract customerInteract;
    public GameObject orderUICanvas;
    public void OrderClick()
    {
        if (customerInteract != null)
        {
            customerInteract.TryFinishOrder();
        }
    }
}
