using UnityEngine;

public class MiniGame : MonoBehaviour, IInteractable
{
    [SerializeField] Material outlineMaterial;
    private MeshRenderer meshRenderer;

    private Material[] originalMaterials;  

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterials = meshRenderer.materials; 
    }

    public void Interact()
    {
        // do your interaction here
    }

    public void OnRaycastEnter()
    {
        // Make a copy so we don’t overwrite the stored reference
        var mats = meshRenderer.materials;

        // If you just want to replace slot 0 with the outline:
        mats[0] = outlineMaterial;
        meshRenderer.materials = mats;
    }

    public void OnRaycastStay()
    {
        // nothing special yet
    }

    public void OnRaycastExit()
    {
        // Restore the original set
        meshRenderer.materials = originalMaterials;
    }
}
