using UnityEngine;
using UnityEngine.UI;
public class ShakeUI : InventoryUI
{
    public GameObject shakeUICanvas;
    public Transform spawnPoint;
    public Slider shakeProgress;
    ShakeJar shake;
    private void Start()
    {
        shake = FindAnyObjectByType<ShakeJar>();
    }

    private void Update()
    {
        if (shake.shakeCount == shake.shakesNeeded)
        {
            
        }
    }
}
