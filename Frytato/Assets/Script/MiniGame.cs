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

   
}
