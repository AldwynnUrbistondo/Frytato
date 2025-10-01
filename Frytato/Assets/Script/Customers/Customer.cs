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
}
