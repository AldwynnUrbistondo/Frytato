using UnityEngine;

public class CookFries : MonoBehaviour
{
    public CookFriesObject cookFriesObject;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MakeKinematicAfterDelay(1f));
    }

    private System.Collections.IEnumerator MakeKinematicAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = true;
    }
}
