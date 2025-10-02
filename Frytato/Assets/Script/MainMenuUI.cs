using Unity.VisualScripting;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] bool starting = true;
    [SerializeField] GameObject tapToPlay;
    [SerializeField] GameObject mainUI;

    private void Update()
    {
        if (starting && Input.GetMouseButtonDown(0))
        {
            tapToPlay.SetActive(false);
            mainUI.SetActive(true);
            starting = false;
        }
    }
}
