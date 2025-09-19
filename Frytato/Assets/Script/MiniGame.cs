using UnityEngine;

public class MiniGame : MonoBehaviour, IInteractable
{
    public GameObject canvas;

    public Material outlineMaterial;
    [HideInInspector] public MeshRenderer meshRenderer;
    [HideInInspector] public Material[] originalMaterials;  

    public void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterials = meshRenderer.materials; 
    }

    public void Interact()
    {
        // do your interaction here
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Material[] newMaterials = new Material[originalMaterials.Length + 1];
            originalMaterials.CopyTo(newMaterials, 0);
            newMaterials[newMaterials.Length - 1] = outlineMaterial;
            meshRenderer.materials = newMaterials;

            canvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshRenderer.materials = originalMaterials;
            canvas.SetActive(false);
        }
    }
}
