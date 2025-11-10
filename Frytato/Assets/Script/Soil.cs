using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Soil : MonoBehaviour, IInteractable
{
    public Plant plant;
    [SerializeField] public PlantState plantState = PlantState.Empty;
    public int soilID;

    public void Interact()
    {
        // Planting
        if (plantState == PlantState.Empty && UIManager.Instance.roamUI.equippedItem != null && UIManager.Instance.roamUI.equippedItem is PotatoObject potatoObj)
        {
            Debug.Log("Planted Soil");

            plant.isGrowing = true;
            plantState = PlantState.Growing;

            // Assign the potato object reference
            plant.potatoObj = potatoObj;
            plant.currentGrowth = 0f;

            // Remove one potato seed from inventory
            InventoryManager.Instance.RemoveItem(potatoObj, 1);
        }
        // Already growing
        else if (plantState == PlantState.Growing)
        {
            Debug.Log("There is a plant growing");
        }
        // Ready to harvest
        else if (plantState == PlantState.Harvest)
        {
            Harvest();
        }

    }
    private void Update()
    {
        if (plant.isGrowing && plant.potatoObj != null)
        {
            Grow();
        }
    }

    private void Grow()
    {
        plant.currentGrowth += Time.deltaTime;

        //Debug.Log($"Plant growth: {plant.currentGrowth:0.0}/{plant.potatoObj.growthTime}");

        if (plant.currentGrowth >= plant.potatoObj.growthTime)
        {
            Debug.Log("Plant is done growing!");
            plant.soilChange.SetActive(true);
            plant.isGrowing = false;
            plantState = PlantState.Harvest;
        }
    }

    private void Harvest()
    {
        plant.harvestAmount = Random.Range(2, 4);

        for (int i = 0; i < plant.harvestAmount; i++)
        {
            Instantiate(plant.potatoObj.dropableItem, plant.harvestSpawnPoint.position, Quaternion.identity);
        }
        plant.soilChange.SetActive(false);
        Debug.Log($"You harvested {plant.harvestAmount} potatoes.");

        // Reset soil
        plantState = PlantState.Empty;
        plant.potatoObj = null;
        plant.currentGrowth = 0f;
    }

    public SoilData GetSoilData()
    {
        SoilData data = new SoilData();
        data.soilID = soilID;
        data.isGrowing = plant.isGrowing;
        data.currentGrowth = plant.currentGrowth;
        data.plantState = plantState;
        data.plantID = plant.potatoObj != null ? plant.potatoObj.itemID : null;
        return data;
    }

    public void LoadSoilData(SoilData data)
    {
        plant.isGrowing = data.isGrowing;
        plant.currentGrowth = data.currentGrowth;
        plantState = data.plantState;

        if (!string.IsNullOrEmpty(data.plantID))
        {
            // Find the matching potato object from your item database
            foreach (var item in ItemDatabase.Instance.itemData)
            {
                if (item.itemID == data.plantID && item is PotatoObject potatoObj)
                {
                    plant.potatoObj = potatoObj;
                    break;
                }
            }
        }
    }

}

public enum PlantState
{
    Empty,
    Growing,
    Harvest
}

[System.Serializable]
public class Plant
{
    public bool isGrowing = false;
    public float currentGrowth = 0f;
    public Transform harvestSpawnPoint;
    public int harvestAmount;
    public PotatoObject potatoObj;

    public GameObject soilChange;
}



