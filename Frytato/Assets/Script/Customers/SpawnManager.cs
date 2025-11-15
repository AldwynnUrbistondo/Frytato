using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("Customer Settings")]
    public GameObject[] customerPrefabs;
    public Transform[] spawnPoints;

    [Header("Queue Lines")]
    public LinePoints[] lines;              // Each line has multiple positions
    private Customer[][] customerLines;     // 2D array: line index -> queue index

    [Header("Done Spot")]
    public Transform doneOrderingSpot;

    [Header("Spawn Settings")]
    public int maxSpawnCount = 20;
    public float spawnInterval = 2f;

    public int activeCustomerCount = 0;  // Track active customers instead
    private float timer = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Initialize customer arrays for each line
        customerLines = new Customer[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            customerLines[i] = new Customer[lines[i].points.Length];
        }
    }

    private void Update()
    {
        // Check against active customers, not total spawned
        if (activeCustomerCount >= maxSpawnCount) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCustomer();
            timer = 0f;
        }
    }

    private void SpawnCustomer()
    {
        // Find the shortest line with an empty spot
        int lineIndex = GetShortestLineIndexWithSpace();

        // If all lines are full, skip spawning
        if (lineIndex == -1)
            return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int prefabIndex = Random.Range(0, customerPrefabs.Length);

        GameObject obj = Instantiate(customerPrefabs[prefabIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
        Customer newCustomer = obj.GetComponent<Customer>();

        // Place customer in first empty spot in that line
        for (int i = 0; i < customerLines[lineIndex].Length; i++)
        {
            if (customerLines[lineIndex][i] == null)
            {
                customerLines[lineIndex][i] = newCustomer;
                newCustomer.queueIndex = i;
                newCustomer.MoveTo(lines[lineIndex].points[i].position);

                // First in line gets an order and is at cashier
                if (i == 0)
                {
                    newCustomer.SetRandomOrder();
                    newCustomer.isAtCashier = true;
                }
                else
                {
                    newCustomer.isAtCashier = false;
                }

                break;
            }
        }

        activeCustomerCount++;  // Increment active count
    }

    // Returns the index of the shortest line that has at least one empty spot
    // Returns -1 if all lines are full
    private int GetShortestLineIndexWithSpace()
    {
        int bestLine = -1;
        int minCount = int.MaxValue;

        for (int i = 0; i < customerLines.Length; i++)
        {
            int count = 0;
            bool hasEmpty = false;

            for (int j = 0; j < customerLines[i].Length; j++)
            {
                if (customerLines[i][j] != null) count++;
                else hasEmpty = true;
            }

            // Skip this line if it has no empty spot
            if (!hasEmpty) continue;

            if (count < minCount)
            {
                minCount = count;
                bestLine = i;
            }
        }

        return bestLine;
    }

    private int GetShortestLineIndex()
    {
        int bestLine = 0;
        int minCount = int.MaxValue;

        for (int i = 0; i < customerLines.Length; i++)
        {
            int count = 0;
            foreach (var customer in customerLines[i])
                if (customer != null) count++;

            if (count < minCount)
            {
                minCount = count;
                bestLine = i;
            }
        }

        return bestLine;
    }

    public void SendCustomerToDoneSpot(Customer c)
    {
        if (doneOrderingSpot != null)
            c.MoveTo(doneOrderingSpot.position);

        int lineIndex = -1;
        int queueIndex = c.queueIndex;

        // Find the line the customer is in
        for (int i = 0; i < customerLines.Length; i++)
        {
            if (queueIndex >= 0 && queueIndex < customerLines[i].Length && customerLines[i][queueIndex] == c)
            {
                lineIndex = i;
                break;
            }
        }

        if (lineIndex >= 0)
        {
            // Mark customer as no longer at cashier
            c.isAtCashier = false;
            customerLines[lineIndex][queueIndex] = null;
            StartCoroutine(ShiftLineAfterDelay(lineIndex, queueIndex, 1f));
        }
    }

    // Call this when a customer leaves completely (after they're done)
    public void OnCustomerLeft(Customer c)
    {
        activeCustomerCount--;
        if (activeCustomerCount < 0) activeCustomerCount = 0;
    }

    private IEnumerator ShiftLineAfterDelay(int lineIndex, int startIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        ShiftLine(lineIndex, startIndex);
    }

    private void ShiftLine(int lineIndex, int startIndex)
    {
        for (int i = startIndex + 1; i < customerLines[lineIndex].Length; i++)
        {
            if (customerLines[lineIndex][i] != null)
            {
                Customer c = customerLines[lineIndex][i];
                customerLines[lineIndex][i - 1] = c;
                customerLines[lineIndex][i] = null;
                c.queueIndex = i - 1;
                c.MoveTo(lines[lineIndex].points[i - 1].position);

                // Update cashier status based on new position
                if (c.queueIndex == 0)
                {
                    c.isAtCashier = true;
                }
                else
                {
                    c.isAtCashier = false;
                }
            }
            else
            {
                break;
            }
        }

        // Assign new order to the new front customer
        if (customerLines[lineIndex][0] != null)
        {
            customerLines[lineIndex][0].SetRandomOrder();
            customerLines[lineIndex][0].isAtCashier = true;
        }
    }
}

[System.Serializable]
public class LinePoints
{
    public Transform[] points; // Positions in this line
}