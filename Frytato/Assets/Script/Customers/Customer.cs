using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    NavMeshAgent customer;


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
}
