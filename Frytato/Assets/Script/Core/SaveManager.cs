using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    public SaveData saveData;
    public List<ItemData> itemsExisitingInScene = new List<ItemData>();

    private bool isSaving = false;

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

    // Use this method when you need to save before scene change
    public void SaveGameAsync(System.Action onComplete = null)
    {
        if (!isSaving)
        {
            StartCoroutine(SaveGameCoroutine(onComplete));
        }
    }

    private IEnumerator SaveGameCoroutine(System.Action onComplete)
    {
        isSaving = true;

        saveData.savedInventory.Clear();

        // Items in Inventory
        foreach (var item in InventoryManager.Instance.items)
        {
            ItemID newSavedItem = new ItemID(item.itemData.itemID);
            newSavedItem.quantity = item.quantity;
            saveData.savedInventory.Add(newSavedItem);
        }

        // Items in Scene
        foreach (var item in itemsExisitingInScene)
        {
            ItemID newSavedItem = new ItemID(item.itemID);
            newSavedItem.quantity = 1;
            saveData.savedInventory.Add(newSavedItem);
        }

        // Soil
        saveData.savedSoils.Clear();
        Soil[] allSoils = FindObjectsByType<Soil>(FindObjectsSortMode.None);
        foreach (var soil in allSoils)
        {
            saveData.savedSoils.Add(soil.GetSoilData());
        }

        string jsonData = JsonUtility.ToJson(saveData);
        string path = GetSavePath();

        // Write to file asynchronously
        yield return StartCoroutine(WriteFileAsync(path, jsonData));

        Debug.Log("Game Saved Successfully!");

        isSaving = false;

        // Call the completion callback (e.g., scene change)
        onComplete?.Invoke();
    }

    // Synchronous save (for quick saves that don't block scene changes)
    public void SaveGame()
    {
        saveData.savedInventory.Clear();

        // Items in Inventory
        foreach (var item in InventoryManager.Instance.items)
        {
            ItemID newSavedItem = new ItemID(item.itemData.itemID);
            newSavedItem.quantity = item.quantity;
            saveData.savedInventory.Add(newSavedItem);
        }

        // Items in Scene
        foreach (var item in itemsExisitingInScene)
        {
            ItemID newSavedItem = new ItemID(item.itemID);
            newSavedItem.quantity = 1;
            saveData.savedInventory.Add(newSavedItem);
        }

        // Soil
        saveData.savedSoils.Clear();
        Soil[] allSoils = FindObjectsByType<Soil>(FindObjectsSortMode.None);
        foreach (var soil in allSoils)
        {
            saveData.savedSoils.Add(soil.GetSoilData());
        }

        string jsonData = JsonUtility.ToJson(saveData);
        File.WriteAllText(GetSavePath(), jsonData);
        Debug.Log("Game Saved Successfully!");
    }

    private IEnumerator WriteFileAsync(string path, string data)
    {
        // Offload file writing to prevent blocking
        bool writeComplete = false;
        System.Exception writeException = null;

        System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                File.WriteAllText(path, data);
            }
            catch (System.Exception e)
            {
                writeException = e;
            }
            finally
            {
                writeComplete = true;
            }
        });

        // Wait for write to complete
        while (!writeComplete)
        {
            yield return null;
        }

        if (writeException != null)
        {
            Debug.LogError("Failed to save: " + writeException.Message);
        }
    }

    public void LoadGame()
    {
        string path = GetSavePath();

        if (File.Exists(path))
        {
            try
            {
                string loadedData = File.ReadAllText(path);
                saveData = JsonUtility.FromJson<SaveData>(loadedData);
                LoadInventory();

                // Load soils
                Soil[] allSoils = FindObjectsByType<Soil>(FindObjectsSortMode.None);
                foreach (var soilData in saveData.savedSoils)
                {
                    foreach (var soil in allSoils)
                    {
                        if (soil.soilID == soilData.soilID)
                        {
                            soil.LoadSoilData(soilData);
                            break;
                        }
                    }
                }

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
        UIManager.Instance.UpdateUI();

        foreach (var saveData in saveData.savedInventory)
        {
            foreach (var dbItem in ItemDatabase.Instance.itemData)
            {
                if (saveData.itemID == dbItem.itemID)
                {
                    InventoryManager.Instance.AddItem(dbItem, saveData.quantity);
                }
            }
        }
    }

    // Mobile-compatible save path
    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "SaveData.json");
    }

    public bool IsSaving()
    {
        return isSaving;
    }
}

[System.Serializable]
public class SaveData
{
    public List<ItemID> savedInventory = new List<ItemID>();
    public List<SoilData> savedSoils = new List<SoilData>();
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

[System.Serializable]
public class SoilData
{
    public int soilID;
    public bool isGrowing;
    public float currentGrowth;
    public string plantID;
    public PlantState plantState;
}