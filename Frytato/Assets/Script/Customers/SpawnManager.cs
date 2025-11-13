using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("Customer Settings")]
    public GameObject[] customerPrefabs; // The type of customers
    public Transform[] spawnPoint; // Where customers will spawn
    private Customer[] customerLine; // The customer will be assigned to this line
    public Transform[] lineQueuePoint; // The location where the customer will go to when lining up

    [Header("Customer Waiting Area")]
    public Transform[] doneOrderingSpot;
    private int doneSpot = 0;

    [Header("Customer Settings")]
    public int spawnCount;
    public int maxSpawnCount;
    public float spawnInterval;


    float timer = 0f;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        customerLine = new Customer[lineQueuePoint.Length];

        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CustomerAmount();
    }

    void CustomerAmount()
    {
        if (spawnCount >= maxSpawnCount)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnCustomer();
            timer = 0f;
        }

    }

    void SpawnCustomer()
    {
        int spawnRandomLoc = Random.Range(0, spawnPoint.Length);
        int spawnRandomCustomer = Random.Range(0, customerPrefabs.Length);
        //Instantiate(customerPrefabs[0], spawnPoint[spawnRandomLoc].position, Quaternion.identity);

        GameObject newCustomerInitialize = Instantiate(customerPrefabs[spawnRandomCustomer], spawnPoint[spawnRandomLoc].position, Quaternion.identity);
        Customer newCustomer = newCustomerInitialize.GetComponent<Customer>();
        for (int i = 0; i < lineQueuePoint.Length; i++)
        {
            if (customerLine[i] == null)
            {
                customerLine[i] = newCustomer;
                newCustomer.MoveTo(lineQueuePoint[i].position);
                break;
            }
        }
        spawnCount++;
    }

    public void SendCustomerToDoneSpot(Customer c)
    {
        if (doneOrderingSpot != null && doneOrderingSpot.Length > 0)
        {
            // Move the finished customer to the done spot
            c.MoveTo(doneOrderingSpot[0].position);
        }

        // Remove from queue
        if (c.queueIndex >= 0 && c.queueIndex < customerLine.Length)
        {
            int leavingIndex = c.queueIndex;
            customerLine[leavingIndex] = null;

            // Start coroutine for delayed line shift
            StartCoroutine(ShiftLineAfterDelay(leavingIndex, 2f)); // 2-second delay
        }
    }

    private IEnumerator ShiftLineAfterDelay(int startIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        ShiftLine(startIndex);
    }

    private void ShiftLine(int startIndex)
    {
        // Move everyone behind the leaving customer forward
        for (int i = startIndex + 1; i < customerLine.Length; i++)
        {
            if (customerLine[i] != null)
            {
                // Move them to the next spot forward
                customerLine[i].MoveTo(lineQueuePoint[i - 1].position);

                // Update their queue index
                customerLine[i].queueIndex = i - 1;

                // Update the array
                customerLine[i - 1] = customerLine[i];
                customerLine[i] = null;
            }
            else
            {
                // If we hit an empty spot, we can stop shifting
                break;
            }
        }

    }
}
