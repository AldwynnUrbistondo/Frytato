using UnityEngine;
public class Soil : MonoBehaviour, IInteractable
{
    public Plant plant;
    [SerializeField] PlantState plantstate = PlantState.Empty;


    public void Interact()
    {
        if (plantstate == PlantState.Empty)
        {
            Debug.Log("Planted Soil");
            plant.isGrowing = true;
            Plant();
        }
        else if (plantstate == PlantState.Growing)
        {
            Debug.Log("There is a plant growing");
        }
        else if (plantstate == PlantState.Harvest)
        {
            Harvest();
        }
    }

    void Update()
    {
        if (plant.isGrowing)
        {
            Plant();
        }
    }

    void Plant()
    {
        plant.potatoObj.growDuration += Time.deltaTime;
        Debug.Log(plant.potatoObj.growDuration);
        plantstate = PlantState.Growing;
        if (plant.potatoObj.growDuration >= 10)
        {
            Debug.Log("Plant is done growing");
            plant.isGrowing = false;
            plant.potatoObj.growDuration = 0;
            plantstate = PlantState.Harvest;
        }
    }

    void Harvest()
    {
        plant.harvestAmount = Random.Range(2, 4);
        for (int baseHarventAmount = 0;  baseHarventAmount < plant.harvestAmount; baseHarventAmount++)
        {
            Instantiate(plant.potatoObj.dropableItem, plant.harvestSpawnPoint.position, Quaternion.identity);
        }
        Debug.Log($"You have harvested {plant.harvestAmount} of potatoes.");
        plantstate = PlantState.Empty;
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
    public Transform harvestSpawnPoint;
    public int harvestAmount;
    public PotatoObject potatoObj;

}


