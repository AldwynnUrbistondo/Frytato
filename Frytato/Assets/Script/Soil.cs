using UnityEngine;
public class Soil : MonoBehaviour, IInteractable
{
    public Plant plant;
    float timer = 0;
    [SerializeField] PlantState plantstate = PlantState.Empty;


    public void Interact()
    {
        if (plantstate == PlantState.Empty && EquipItem.Instance.equippedItem is PotatoObject && EquipItem.Instance.equippedItem != null)
        {
            Debug.Log("Planted Soil");
            plant.isGrowing = true;
            plantstate = PlantState.Growing;

            // Set the plant's potatoObj to the equipped item
            plant.potatoObj = (PotatoObject)EquipItem.Instance.equippedItem;

            // Remove one potato seed from inventory
            InventoryManager.Instance.RemoveItem(EquipItem.Instance.equippedItem, 1);

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
        timer += Time.deltaTime;
        Debug.Log(plant.potatoObj.growDuration);
        if (timer >= plant.potatoObj.growDuration)
        {
            Debug.Log("Plant is done growing");
            plant.isGrowing = false;
            timer = 0;
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


