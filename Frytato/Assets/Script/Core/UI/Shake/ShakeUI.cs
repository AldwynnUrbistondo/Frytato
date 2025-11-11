using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShakeUI : InventoryUI
{
    public GameObject shakeUICanvas;
    public Transform spawnPoint;
    public Slider shakeProgress;
    ShakeJar shake;
    public TextMeshProUGUI friesCountText;
    private void Start()
    {
        shake = FindAnyObjectByType<ShakeJar>();
        friesCountText.color = Color.red;
    }

    private void Update()
    {
        FriesIndicator();
    }

    void FriesIndicator()
    {
        friesCountText.text = $"Fries needed: {ShakeManager.Instance.friesinJarCount.ToString()}/10";

        if (ShakeManager.Instance.friesinJarCount == 10)
        {
            friesCountText.color = Color.green;
        }
        else
        {
            friesCountText.color = Color.red;
        }
    }

    
}
