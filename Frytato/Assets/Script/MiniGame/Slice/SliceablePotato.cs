using UnityEngine;

public class SliceablePotato : MonoBehaviour
{
    [SerializeField] GameObject[] variants;
    [SerializeField] Transform[] spawnpoints;
    int currentIndex = 0;

    public PotatoObject potatoObject;
    private SlicedFries myFriesRecord;

    void Start()
    {
        // Register this potato with the manager
        myFriesRecord = new SlicedFries
        {
            slicablePotato = this,
            fries = new GameObject[0] // start empty
        };
        SlicedFriesManager.Instance.slicedFriesList.Add(myFriesRecord);

        // Show only the first variant
        for (int i = 0; i < variants.Length; i++)
        {
            variants[i].SetActive(i == 0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SlicePotato();
        }
    }

    public void SlicePotato()
    {
        if (currentIndex < variants.Length - 1)
        {
            Vector3 pos = variants[currentIndex].transform.position;
            Quaternion rot = variants[currentIndex].transform.rotation;

            variants[currentIndex].SetActive(false);
            SpawnFries();

            currentIndex++;
            variants[currentIndex].SetActive(true);
            variants[currentIndex].transform.SetPositionAndRotation(pos, rot);
        }
        else
        {
            SpawnFries();
            // When destroyed, tell manager to clear fries
            SlicedFriesManager.Instance.ClearFries(this);
            Destroy(gameObject);
        }
    }

    void SpawnFries()
    {
        GameObject[] newFries = new GameObject[potatoObject.fryAmount];

        for (int i = 0; i < potatoObject.fryAmount; i++)
        {
            int spawnIndex = Random.Range(0, spawnpoints.Length);
            GameObject fry = Instantiate(
                potatoObject.fryObject,
                spawnpoints[spawnIndex].position,
                Quaternion.identity
            );
            newFries[i] = fry;
        }

        // Append spawned fries to manager’s record
        int oldLength = myFriesRecord.fries.Length;
        System.Array.Resize(ref myFriesRecord.fries, oldLength + newFries.Length);
        newFries.CopyTo(myFriesRecord.fries, oldLength);
    }
}
