using System.Collections.Generic;
using UnityEngine;

public class SlicedFriesManager : MonoBehaviour
{
    public static SlicedFriesManager Instance;

    [SerializeField]
    public List<SlicedFries> slicedFriesList = new List<SlicedFries>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ClearFries(SliceablePotato potato)
    {
        SlicedFries record = slicedFriesList.Find(x => x.slicablePotato == potato);

        if (record != null)
        {
            // Example: Destroy all fries
            foreach (GameObject fry in record.fries)
            {
                if (fry != null)
                {
                    Collider col = fry.GetComponent<Collider>();
                    col.enabled = false;

                    Rigidbody rb = fry.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                    rb.linearVelocity = Vector3.right * 5;

                    RawFries friesComponent = fry.GetComponent<RawFries>();
                    InventoryManager.Instance.AddItem(friesComponent.friesObject, 1);

                    Destroy(fry, 2f);
                }
            }

            // Remove record from the list
            slicedFriesList.Remove(record);
        }
    }

}

[System.Serializable]
public class SlicedFries
{
    public SliceablePotato slicablePotato;
    public GameObject[] fries;
}
