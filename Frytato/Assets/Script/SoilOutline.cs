using UnityEngine;
using UnityEngine.UI;
public class SoilOutline : MonoBehaviour
{
    Soil soil;
    public GameObject sliderObj;
    public GameObject outlineObject;
    public GameObject finishedPotatoIcon;
    public Slider slider;

    void Start()
    {
        finishedPotatoIcon.SetActive(false);
        soil = GetComponent<Soil>();
        slider.value = 0f;
        sliderObj.SetActive(false);
        outlineObject.SetActive(false);
    }

    private void Update()
    {
        SliderCheck();
        SliderVisual();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outlineObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outlineObject.SetActive(false);
        }
    }

    void SliderCheck()
    {
        if (soil.plant.potatoObj != null)
            slider.value = soil.plant.currentGrowth / soil.plant.potatoObj.growthTime;
        else
            slider.value = 0f;
    }

    void SliderVisual()
    {
        if (soil.plantState == PlantState.Growing)
        {
            sliderObj.SetActive(true);
        }
        else if (soil.plantState == PlantState.Harvest)
        {
            finishedPotatoIcon.SetActive(true);
            sliderObj.SetActive(false);
        }
        else
        {
            finishedPotatoIcon.SetActive(false);
        }
    }
}
