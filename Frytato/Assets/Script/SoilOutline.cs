using UnityEngine;

public class SoilOutline : MonoBehaviour
{
    public GameObject canvas;
    public GameObject outlineObject;
    void Start()
    {
        canvas.SetActive(false);
        outlineObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true);
            outlineObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
        outlineObject.SetActive(false);
    }
}
