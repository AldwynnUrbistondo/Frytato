using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    NavMeshAgent customer;
    public int queueIndex;

    private void Awake()
    {
        customer = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    public void MoveTo(Vector3 target)
    {
        customer.SetDestination(target);
    }

    public void FinishedOrdering()
    {
        SpawnManager.Instance.SendCustomerToDoneSpot(this);
    }
}
