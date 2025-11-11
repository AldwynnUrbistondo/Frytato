using UnityEngine;

[CreateAssetMenu(fileName = "New Fries Object", menuName = "Items/Fries Object")]

public class RawFriesObject : ItemData
{
    [Header("Raw Fries Specific")]
    public FriesTexData cookingMaterial;

    public CookFriesObject Raw;
    public CookFriesObject Undercook;
    public CookFriesObject Cook;
    public CookFriesObject Overcook;
    public CookFriesObject Burnt;
}
