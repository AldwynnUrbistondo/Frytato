using UnityEngine;

[CreateAssetMenu(fileName = "New Powder Fries Object", menuName = "Items/Powder Fries Object")]

public class PowderFries : ItemData
{
    [Header("Powder Fries Specific")]
    public CookState cookState;
    public Flavor friesFlavor;

    public Texture2D cookTexture;
    public Material powderMaterial;
}

public enum Flavor
{
    Cheese,
    BBQ,
    SourCream
}
