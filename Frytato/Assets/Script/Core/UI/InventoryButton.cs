using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    Button button;
    public ItemData itemData;

    public virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public virtual void OnClick()
    {

    }
}
