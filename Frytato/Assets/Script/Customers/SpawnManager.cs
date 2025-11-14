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

    private int spawnedCount = 0;
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
        if (spawnedCount >= maxSpawnCount) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCustomer();
            timer = 0f;
        }
    }

    private void SpawnCustomer()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int prefabIndex = Random.Range(0, customerPrefabs.Length);

        GameObject obj = Instantiate(customerPrefabs[prefabIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
        Customer newCustomer = obj.GetComponent<Customer>();

        // Pick the shortest line
        int lineIndex = GetShortestLineIndex();

        // Place customer in first empty spot in that line
        for (int i = 0; i < customerLines[lineIndex].Length; i++)
        {
            if (customerLines[lineIndex][i] == null)
            {
                customerLines[lineIndex][i] = newCustomer;
                newCustomer.queueIndex = i;
                newCustomer.MoveTo(lines[lineIndex].points[i].position);

                // First in line gets an order
                if (i == 0)
                    newCustomer.SetRandomOrder();

                break;
            }
        }

        spawnedCount++;
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
            customerLines[lineIndex][queueIndex] = null;
            StartCoroutine(ShiftLineAfterDelay(lineIndex, queueIndex, 1f));
        }
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
            }
            else
            {
                break;
            }
        }

        // Assign new order to the new front customer
        if (customerLines[lineIndex][0] != null)
            customerLines[lineIndex][0].SetRandomOrder();
    }
}

[System.Serializable]
public class LinePoints
{
    public Transform[] points; // Positions in this line
}