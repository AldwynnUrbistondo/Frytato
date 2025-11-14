using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour, IInteractable
{
    private NavMeshAgent agent;
    public int queueIndex = -1; // Track position in line

    [Header("Order UI")]
    public GameObject[] orderFries; // UI objects above the head
    private GameObject currentOrder;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Rotate toward movement direction
        if (agent.desiredVelocity.magnitude > 0.1f)
        {
            Vector3 lookDir = new Vector3(agent.desiredVelocity.x, 0, agent.desiredVelocity.z);
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    public void MoveTo(Vector3 target)
    {
        if (agent != null)
            agent.SetDestination(target);
    }

    // Assign a random order (called when customer reaches front of line)
    public void SetRandomOrder()
    {
        if (orderFries.Length == 0) return;

        // Disable all fries first
        foreach (GameObject fries in orderFries)
            fries.SetActive(false);

        int randomIndex = Random.Range(0, orderFries.Length);
        currentOrder = orderFries[randomIndex];
        currentOrder.SetActive(true);
    }

    // Called when player takes the order
    public void OrderTaken()
    {
        if (currentOrder != null)
        {
            currentOrder.SetActive(false);
            currentOrder = null;
        }

        SpawnManager.Instance.SendCustomerToDoneSpot(this);
    }

    public void Interact()
    {
        OrderTaken();
    }
}