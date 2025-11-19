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
    public TextMeshProUGUI indicatorState;
    public GameObject updownIndicator;
    public ShakeJar jar;
    private void Start()
    {
        shake = FindAnyObjectByType<ShakeJar>();
        friesCountText.color = Color.red;
    }

    private void Update()
    {
        FriesIndicator();
        Indication();
    }

    void FriesIndicator()
    {
        friesCountText.text = $"{ShakeManager.Instance.friesinJarCount.ToString()}/10";

        if (shake.canShake)
        {
            updownIndicator.SetActive(true);
        }
        else
        {
            updownIndicator.SetActive(false);
        }

        if (ShakeManager.Instance.friesinJarCount == 10)
        {
            friesCountText.color = Color.green;
        }
        else
        {
            updownIndicator.SetActive(false);
            friesCountText.color = Color.red;
        }
    }

    void Indication()
    {
        if (ShakeManager.Instance.currentFriesCount == 10 && ShakeManager.Instance.hasFlavor && jar.finishedShaking)
        {
            indicatorState.text = "Put the jar into the fries container on the right";
        }
        else if (ShakeManager.Instance.currentFriesCount == 10 && ShakeManager.Instance.hasFlavor)
        {
            indicatorState.text = "Shake the jar";
        }
        else if (ShakeManager.Instance.currentFriesCount == 10)
        {
            indicatorState.text = "Drag a flavor on top of the jar!";
        }
        else
        {
            indicatorState.text = "";
        }


    }
}
