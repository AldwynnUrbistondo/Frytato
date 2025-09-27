using UnityEngine;

[CreateAssetMenu(fileName = "New Fries Texture Data", menuName = "Items/Fries Texture Data")]
public class FriesTexData : ScriptableObject
{
    public Texture2D rawTex;
    public Texture2D undercookTex;
    public Texture2D cookTex;
    public Texture2D overcookTex;
    public Texture2D burntTex;
}
