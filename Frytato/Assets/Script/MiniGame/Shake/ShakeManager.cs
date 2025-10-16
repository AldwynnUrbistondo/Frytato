using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    public static ShakeManager Instance { get; private set; }

    [Header("Shake Settings")]
    public int friesLimit = 10;
    private int currentFriesCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    public bool CanAddFries()
    {
        return currentFriesCount < friesLimit;
    }
    
    public void AddFries()
    {
        currentFriesCount++;
    }

    public void ResetFriesCount()
    {
        currentFriesCount = 0;
    }
}
