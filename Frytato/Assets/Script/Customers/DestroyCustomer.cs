using UnityEngine;

public class DestroyCustomer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            
            Destroy(other.gameObject);
        }
    }
}
