using UnityEngine;

public class PotatoBehaviour : MonoBehaviour
{
    [SerializeField] GameObject[] variants; // Assign in Inspector: Whole -> Half -> Quarter -> etc.
    int currentIndex = 0;

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

    void SlicePotato()
    {
        if (currentIndex < variants.Length - 1)
        {
            // Save transform before switching
            Vector3 pos = variants[currentIndex].transform.position;
            Quaternion rot = variants[currentIndex].transform.rotation;

            // Deactivate current
            variants[currentIndex].SetActive(false);

            // Activate next
            currentIndex++;
            variants[currentIndex].SetActive(true);

            // Apply same position/rotation so it doesn’t move
            variants[currentIndex].transform.SetPositionAndRotation(pos, rot);
        }
        else
        {
            Debug.Log("Potato is fully sliced!");
        }
    }
}
