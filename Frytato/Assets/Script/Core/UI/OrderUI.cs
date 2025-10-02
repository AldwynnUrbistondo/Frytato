using UnityEngine;

public class OrderUI : MonoBehaviour
{
    public CustomerInteract customerInteract;
    public Canvas orderUICanvas;
    public void OrderClick()
    {
        if (customerInteract != null)
        {
            customerInteract.TryFinishOrder();
        }
    }
}
