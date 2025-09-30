using UnityEngine;

public class CustomerInteract : MonoBehaviour
{
    private Customer currentCustomer;
    private float timerInteract;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timerInteract += Time.deltaTime;   
        if (currentCustomer != null && Input.GetKeyDown(KeyCode.Space))
        {
            if (timerInteract > 2)
            {
                Debug.Log("Customer has ordered");
                currentCustomer.FinishedOrdering();
                timerInteract = 0;
            }
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
