using UnityEngine;

[CreateAssetMenu(fileName = "New Cook Fries Object", menuName = "Items/Cook Fries Object")]
public class CookFriesObject : ItemData
{
    public CookState cookState;
}

public enum CookState
{
    Raw,
    Undercook,
    Cook,
    Overcook,
    Burnt
}
