using UnityEngine;

public class SliceUI : InventoryUI
{

    public static SliceUI Instance;

    public GameObject sliceUICanvas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
