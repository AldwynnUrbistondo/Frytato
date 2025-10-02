using UnityEngine;
using UnityEngine.UI;

public class BackToRoamButton : MonoBehaviour
{
    Button backButton;

    private void Awake()
    {
        backButton = GetComponent<Button>();
        backButton.onClick.AddListener(GameManager.Instance.BackToRoamButton);
    }
}
