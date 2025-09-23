using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Soil : MonoBehaviour, IInteractable
{
    public Plant plant;
    [SerializeField] public PlantState plantState = PlantState.Empty;

    public void Interact()
    {
        // Planting
        if (plantState == PlantState.Empty && RoamUI.Instance.equippedItem != null && RoamUI.Instance.equippedItem is PotatoObject potatoObj)
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

        Debug.Log($"You harvested {plant.harvestAmount} potatoes.");

        // Reset soil
        plantState = PlantState.Empty;
        plant.potatoObj = null;
        plant.currentGrowth = 0f;
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
}



