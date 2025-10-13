using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public SaveData saveData;

    public List<ItemData> itemsExisitingInScene = new List<ItemData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    void SaveGame()
    {
        saveData.savedInventory.Clear(); // Clear previous data

        foreach (var item in InventoryManager.Instance.items)
        {
            ItemID newSavedItem = new ItemID(item.itemData.itemID);
            newSavedItem.quantity = item.quantity; // Save the quantity
            saveData.savedInventory.Add(newSavedItem);
        }

        foreach(var item in itemsExisitingInScene)
        {
            ItemID newSavedItem = new ItemID(item.itemID);
            newSavedItem.quantity = 1;
            saveData.savedInventory.Add(newSavedItem);
        }

        string jsonData = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.dataPath + "/SaveData.text", jsonData);

        Debug.Log("Game Saved Succesfully!");
    }

    void LoadGame()
    {
        string path = Application.dataPath + "/SaveData.text"; // Add slash
        if (File.Exists(path))
        {
            try
            {
                string loadedData = File.ReadAllText(path);
                saveData = JsonUtility.FromJson<SaveData>(loadedData);

                LoadInventory();

                Debug.Log("Game Loaded Successfully!");
            }
            catch
            {
                Debug.Log("Can't Read File");
            }
        }
        else
        {
            Debug.Log("Can't Find File!");
        }
    }

    void LoadInventory()
    {
        InventoryManager.Instance.items.Clear();
        foreach(var saveData in saveData.savedInventory)
        {
            foreach(var dbItem in ItemDatabase.Instance.itemData)
            {
                if(saveData.itemID == dbItem.itemID)
                {
                    InventoryManager.Instance.AddItem(dbItem, saveData.quantity);
                }
            }
        }
    }
}

[System.Serializable]
public class SaveData
{
    public List<ItemID> savedInventory = new List<ItemID>();
}

[System.Serializable]
public class ItemID
{
    public string itemID;
    public int quantity;

    public ItemID(string itemID)
    {
        this.itemID = itemID;
    }
}
