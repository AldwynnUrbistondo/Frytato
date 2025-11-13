using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    NavMeshAgent customer;
    public int queueIndex;

    [Header("UI References")]
    public GameObject[] orderFries;

    // Keep track of which fries was chosen
    private GameObject currentOrder;

    private void Awake()
    {
        customer = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 desiredVelocity = customer.desiredVelocity;

        if (desiredVelocity.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(desiredVelocity.x, 0, desiredVelocity.z));
        }
    }

    public void MoveTo(Vector3 target)
    {
        customer.SetDestination(target);
    }

    public void FinishedOrdering()
    {
        SpawnManager.Instance.SendCustomerToDoneSpot(this);
    }

    public void SetRandomOrder()
    {
        if (orderFries.Length == 0) return;

        // Disable all fries first
        foreach (GameObject fries in orderFries)
        {
            fries.SetActive(false);
        }

        // Pick a random fries
        int randomIndex = Random.Range(0, orderFries.Length);
        currentOrder = orderFries[randomIndex];
        currentOrder.SetActive(true);
    }

    public void OrderTaken()
    {
        // Disable the current fries order
        if (currentOrder != null)
        {
            currentOrder.SetActive(false);
            currentOrder = null;
        }
    }
}