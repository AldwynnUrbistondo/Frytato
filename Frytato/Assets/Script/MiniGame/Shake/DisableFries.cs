using UnityEngine;

public class DisableFries : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered has the CookFries script
        CookFries cookFries = other.GetComponent<CookFries>();

        if (cookFries != null)
        {
            // The object has the CookFries script, do something with it
            Debug.Log("CookFries object entered the trigger!");

            // Example: disable the object
            cookFries.gameObject.SetActive(false);

            // Or do something with its Rigidbody
            // cookFries.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
