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
    }

    public void TryFinishOrder()
    {
        if (currentCustomer != null && timerInteract > 5f)
        {
            Debug.Log("Customer has ordered");
            //currentCustomer.FinishedOrdering();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            currentCustomer = other.GetComponent<Customer>();
            currentCustomer.SetRandomOrder();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            currentCustomer.OrderTaken();
            if (other.GetComponent<Customer>() == currentCustomer)
            {
                currentCustomer = null;
            }
        }
    }


}
