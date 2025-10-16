using UnityEngine;

public class FindScript : MonoBehaviour
{
    void Start()
    {
        // Replace MyScript with the name of the script you're looking for
        SliceInventoryButton target = FindFirstObjectByType<SliceInventoryButton>();

        if (target != null)
        {
            Debug.Log("Found object with MyScript: " + target.gameObject.name);
        }
        else
        {
            Debug.Log("No object with MyScript found in the scene.");
        }
    }
}
