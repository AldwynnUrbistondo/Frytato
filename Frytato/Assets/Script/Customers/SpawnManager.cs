using System.Threading;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance {get; private set;}

    [Header("Customer Settings")]
    public GameObject[] customerPrefabs;
    public Transform[] spawnPoint;
    private GameObject[] customerLine;
    public Transform[] lineQueuePoint;

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
        customerLine = new GameObject[lineQueuePoint.Length];

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
        //Instantiate(customerPrefabs[0], spawnPoint[spawnRandomLoc].position, Quaternion.identity);

        GameObject newCustomer = Instantiate(customerPrefabs[0],spawnPoint[spawnRandomLoc].position,Quaternion.identity);
        for (int i = 0; i < lineQueuePoint.Length; i++)
        {
            if (customerLine[i] == null)
            {
                customerLine[i] = newCustomer;
                newCustomer.GetComponent<Customer>().MoveTo(lineQueuePoint[i].position);
                break;
            }
        }
        spawnCount++;
    }

    void LineQueue()
    {
        
    }

}
