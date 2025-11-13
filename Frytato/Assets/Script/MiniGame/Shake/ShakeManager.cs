using System.Collections.Generic;
using UnityEngine;
 

public class ShakeManager : MonoBehaviour
{
    public static ShakeManager Instance { get; private set; }

    [Header("Shake Settings")]
    public int friesLimit = 10;
    private int currentFriesCount = 0;
    public int friesinJarCount = 0;
    public bool isAddingFries = false;

    public List<FriesInJar> friesInJar = new List<FriesInJar>();

    public Flavor flavor;
    public bool hasFlavor = false;
    public bool isDone = false;

    private void Awake()
    {
        Instance = this;
    }

    public bool CanAddFries()
    {
        currentFriesCount = friesinJarCount;
        return currentFriesCount < friesLimit;
    }
    
    public void AddFries()
    {
        currentFriesCount++;
        friesinJarCount++;
    }

    public void ResetFriesCount()
    {
        currentFriesCount = 0;
    }

    public void AddFriesToJar(CookFriesObject friesData, GameObject friesObject)
    {
        FriesInJar fries = new FriesInJar(friesData, friesObject);
        friesInJar.Add(fries);

        SaveManager.Instance.itemsExisitingInScene.Add(friesData);
    }


}

[System.Serializable]
public class FriesInJar
{
    public CookFriesObject friesData;
    public GameObject friesObject;

    public FriesInJar(CookFriesObject friesData, GameObject friesObject)
    {
        this.friesData = friesData;
        this.friesObject = friesObject;
    }
}
