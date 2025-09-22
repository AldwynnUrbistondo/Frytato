using UnityEngine;

[CreateAssetMenu(fileName = "New Potato Object", menuName = "Items/Potato Object")]
public class PotatoObject : ItemData
{
    public GameObject dropableItem;
    public float growDuration;  
}

