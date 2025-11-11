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
    public GameObject updownIndicator;
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
        friesCountText.text = $"{ShakeManager.Instance.friesinJarCount.ToString()}/10";

        if (ShakeManager.Instance.friesinJarCount == 10)
        {
            updownIndicator.SetActive(true);
            friesCountText.color = Color.green;
        }
        else
        {
            updownIndicator.SetActive(false);
            friesCountText.color = Color.red;
        }
    }

    
}
