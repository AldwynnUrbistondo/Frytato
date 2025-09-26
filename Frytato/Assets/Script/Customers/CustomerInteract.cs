using UnityEngine;

public class CustomerInteract : MonoBehaviour
{
    private Customer currentCustomer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentCustomer != null && Input.GetKeyDown(KeyCode.Space))
        {
            currentCustomer.FinishedOrdering();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            currentCustomer = other.GetComponent<Customer>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            if (other.GetComponent<Customer>() == currentCustomer)
            {
                currentCustomer = null;
            }
        }
    }


}
