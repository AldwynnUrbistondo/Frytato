using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour, IInteractable
{
    private NavMeshAgent agent;
    public int queueIndex = -1; // Track position in line
    public GameObject outline;

    [Header("Order UI")]
    public Image orderFries; // UI objects above the head
    public Sprite[] flavorUI;
    public Sprite[] satisfactionUI;
    public bool isAtCashier = false;
    bool hasTakenOrder = false;
    public Collider col;

    [Header("Order Details")]
    Flavor orderFlavor;
    int satisfactionRate = 0;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<BoxCollider>();
        col.enabled = false;

        GenerateRandomOrder();
    }

    private void Update()
    {
        // Only rotate toward movement direction if not at cashier
        if (!isAtCashier && agent.desiredVelocity.magnitude > 0.1f)
        {
            Vector3 lookDir = new Vector3(-agent.desiredVelocity.x, 0, agent.desiredVelocity.z);
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        // Only update cashier status if order hasn't been taken yet
        if (!hasTakenOrder)
        {
            UpdateCashierStatus();
        }
    }

    #region Movement and Queueing
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
                    col.enabled = true;

                    // Set rotation to Y 90 when at cashier
                    transform.rotation = Quaternion.Euler(0, 90, 0);

                    if (orderFries != null)
                        orderFries.enabled = true;
                }
            }
            else
            {
                isAtCashier = false;
                col.enabled = false;
                if (orderFries != null)
                    orderFries.enabled = false;
            }
        }
        else
        {
            isAtCashier = false;
            col.enabled = false;
            if (orderFries != null)
                orderFries.enabled = false;
        }
    }
    #endregion

    void GenerateRandomOrder()
    {
        // Randomly select a flavor for the order
        int randomFlavor = Random.Range(0, 3);
        switch (randomFlavor)
        {
            case 0:
                orderFlavor = Flavor.Cheese;
                orderFries.sprite = flavorUI[0];
                break;
            case 1:
                orderFlavor = Flavor.BBQ;
                orderFries.sprite = flavorUI[1];
                break;
            case 2:
                orderFlavor = Flavor.SourCream;
                orderFries.sprite = flavorUI[2];
                break;
        }
    }

    // Called when player takes the order
    public void OrderTaken()
    {
        SatisfactionComputation();
        InventoryManager.Instance.RemoveItem(UIManager.Instance.roamUI.equippedItem, 1);
        hasTakenOrder = true;
        isAtCashier = false;

        // Image stays enabled after taking order (removed the disable line)

        // Collider stays enabled after taking order
        SpawnManager.Instance.SendCustomerToDoneSpot(this);
        SpawnManager.Instance?.OnCustomerLeft(this);
        Destroy(gameObject, 10f);
    }

    void SatisfactionComputation()
    {
        if (UIManager.Instance.roamUI.equippedItem is PowderFries)
        {
            PowderFries powderFries = UIManager.Instance.roamUI.equippedItem as PowderFries;
            if (powderFries.friesFlavor == orderFlavor)
            {
                satisfactionRate += 2; // Correct flavor
            }

            if (powderFries.cookState == CookState.Cook)
            {
                satisfactionRate += 2; // Perfectly cooked
            }
            else if (powderFries.cookState == CookState.Undercook || powderFries.cookState == CookState.Overcook)
            {
                satisfactionRate += 1; // Acceptable cook
            }

            if (satisfactionRate >= 4)
            {
                orderFries.sprite = satisfactionUI[0]; // Happy
                AudioManager.Instance.PlaySound(SoundType.HappyCustomer);
            }
            else if (satisfactionRate >= 2)
            {
                orderFries.sprite = satisfactionUI[1]; // Neutral
                AudioManager.Instance.PlaySound(SoundType.NeutralCustomer);
            }
            else
            {
                orderFries.sprite = satisfactionUI[2]; // Angry
                AudioManager.Instance.PlaySound(SoundType.MadCustomer);
            }
        }
    }

    public void Interact()
    {
        if (isAtCashier && UIManager.Instance.roamUI.equippedItem as PowderFries)
            OrderTaken();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.SetActive(false);
        }
    }

}