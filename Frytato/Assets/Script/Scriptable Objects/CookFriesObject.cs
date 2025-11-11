using UnityEngine;

[CreateAssetMenu(fileName = "New Cook Fries Object", menuName = "Items/Cook Fries Object")]
public class CookFriesObject : ItemData
{
    [Header("Cook Fries Specific")]
    public CookState cookState;
    public Texture2D cookTexture;

    public PowderFries CheeseFries;
    public PowderFries BBQFries;
    public PowderFries SourCreamFries;
}

public enum CookState
{
    Raw,
    Undercook,
    Cook,
    Overcook,
    Burnt
}
