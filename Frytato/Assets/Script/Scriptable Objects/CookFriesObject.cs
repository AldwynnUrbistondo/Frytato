using UnityEngine;

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
