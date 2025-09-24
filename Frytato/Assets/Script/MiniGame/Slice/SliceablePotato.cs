using UnityEngine;

public class SliceablePotato : MonoBehaviour
{
    [SerializeField] GameObject[] variants; 
    [SerializeField] Transform[] spawnpoints;
    int currentIndex = 0;
    public PotatoObject potatoObject;

    void Start()
    {
        // Only show the first variant at the beginning
        for (int i = 0; i < variants.Length; i++)
        {
            variants[i].SetActive(i == 0);
        }
            
    }

    private void Update()
    {
        // For testing: Press space to slice the potato
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SlicePotato();
        }
    }

    public void SlicePotato()
    {
        if (currentIndex < variants.Length - 1)
        {
            // Save transform before switching
            Vector3 pos = variants[currentIndex].transform.position;
            Quaternion rot = variants[currentIndex].transform.rotation;

            // Deactivate current
            variants[currentIndex].SetActive(false);

            SpawnFries();

            // Activate next
            currentIndex++;
            variants[currentIndex].SetActive(true);

            // Apply same position/rotation so it doesn’t move
            variants[currentIndex].transform.SetPositionAndRotation(pos, rot);
        }
        else
        {
            SpawnFries();
            Destroy(gameObject);
        }
    }

    void SpawnFries()
    {
        for(int i = 0; i < potatoObject.fryAmount; i++)
        {
            int spawnIndex = Random.Range(0, spawnpoints.Length);
            Instantiate(potatoObject.fryObject, spawnpoints[currentIndex].position, Quaternion.identity);
        }
    }
}
