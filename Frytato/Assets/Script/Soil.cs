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
        plant.growDuration += Time.deltaTime;
        plant.isGrowing = true;
        Debug.Log(plant.growDuration);
        plantstate = PlantState.Growing;
        if (plant.growDuration >= 10)
        {
            Debug.Log("Plant is done growing");
            plant.isGrowing = false;
            plant.growDuration = 0;
            plantstate = PlantState.Harvest;
        }
    }

    void Harvest()
    {
        plant.harvestAmount = Random.Range(1, 3);
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
    public float growDuration;
    public bool isGrowing = false;
    public int harvestAmount;
    public int potatoRarity;
}


