using System.Collections;
using UnityEngine;

public class RawFries : MonoBehaviour
{
    public RawFriesObject rawFriesObject;

    Collider col;
    Rigidbody rb;

    private void Awake()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public void AddToInventory()
    {

        StartCoroutine(StartToAddInInvetory());
    }

    IEnumerator StartToAddInInvetory()
    {
        float delay = Random.Range(1f, 3f);
        yield return new WaitForSeconds(delay);
        col.enabled = false;
        rb.useGravity = false;
        InventoryManager.Instance.AddItem(rawFriesObject, 1);
        Destroy(gameObject);
    }
}
