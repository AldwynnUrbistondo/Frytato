using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour, IInteractable
{
    private NavMeshAgent agent;
    public int queueIndex = -1; // Track position in line

    [Header("Order UI")]
    public Image orderFries; // UI objects above the head
    public bool isAtCashier = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Rotate toward movement direction
        if (agent.desiredVelocity.magnitude > 0.1f)
        {
            Vector3 lookDir = new Vector3(-agent.desiredVelocity.x, 0, agent.desiredVelocity.z);
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        UpdateCashierStatus();
    }

    public void MoveTo(Vector3 target)
    {
        if (agent != null)
            agent.SetDestination(target);
    }

    // Assign a random order (called when customer reaches front of line)
    public void SetRandomOrder()
    {
        if (orderFries == null) return;
        int randomIndex = Random.Range(0, 3);

        // Set isAtCashier to true when order is assigned (only happens at front)
        isAtCashier = true;
    }

    // Called when player takes the order
    public void OrderTaken()
    {
        SpawnManager.Instance.SendCustomerToDoneSpot(this);
        Destroy(gameObject, 10f);
        SpawnManager.Instance?.OnCustomerLeft(this);
        orderFries.enabled = false;
        isAtCashier = false;
    }

    public void Interact()
    {
        OrderTaken();
    }

    // Update cashier status based on queue position
    private void UpdateCashierStatus()
    {
        // Only customers at position 0 should be at cashier
        if (queueIndex == 0)
        {
            // Check if customer has reached their destination
            if (agent != null && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    isAtCashier = true;
                    if (orderFries != null)
                        orderFries.enabled = true;
                }
            }
            else
            {
                isAtCashier = false;
                if (orderFries != null)
                    orderFries.enabled = false;
            }
        }
        else
        {
            isAtCashier = false;
            if (orderFries != null)
                orderFries.enabled = false;
        }
    }
}