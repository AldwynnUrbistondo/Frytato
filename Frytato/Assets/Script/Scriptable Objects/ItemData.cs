using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string itemID;
    public Sprite itemIcon;
    public GameObject itemObject;
    public Rarity itemRarity;
    public ItemType itemType;
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
public enum ItemType
{
    Potato,
    RawFries,
    CookedFries,
    FlavoredFries,
    PackagedFries
}

