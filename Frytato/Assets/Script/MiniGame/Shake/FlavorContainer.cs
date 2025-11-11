
using UnityEngine;
using System.Collections;
public class FlavorContainer : MonoBehaviour
{
    public Flavor flavor;

    [Header("Return Settings")]
    [SerializeField] private float returnSpeed = 5f;
    Animator anim;

    private DragAndDrop dragScript;
    private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Coroutine returnRoutine;

    private void Start()
    {
        dragScript = GetComponent<DragAndDrop>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        // Save the initial transform
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {

        if (dragScript == null) return;

       
        if (dragScript.isDragging)
        {
            // Stop the return coroutine while dragging
            if (returnRoutine != null)
            {
                StopCoroutine(returnRoutine);
                returnRoutine = null;
            }

            //  Disable animator during drag so it doesn’t override transform
            if (anim.enabled)
                anim.enabled = false;
        }
        else
        {
            // If not dragging and not yet returning, move back to original position
            if (returnRoutine == null)
            {
                returnRoutine = StartCoroutine(ReturnToOrigin());
            }

            if (!anim.enabled)
                anim.enabled = true;
        }
    }

    IEnumerator ReturnToOrigin()
    {
        float t = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        
        rb.constraints = RigidbodyConstraints.FreezeAll;

        while (t < 1f)
        {
            t += Time.deltaTime * returnSpeed;
            transform.position = Vector3.Lerp(startPos, originalPosition, t);
            transform.rotation = Quaternion.Slerp(startRot, originalRotation, t);
            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;

        
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlavorCollider"))
        {
            if (flavor == Flavor.SourCream && !ShakeManager.Instance.hasFlavor && ShakeManager.Instance.friesinJarCount == 10)
            {
                ShakeManager.Instance.hasFlavor = true;
                dragScript.isDragging = false;
                StartCoroutine(PlayPourAnimation());
                Debug.Log("SourCream added");

            }
            else if (flavor == Flavor.BBQ && !ShakeManager.Instance.hasFlavor && ShakeManager.Instance.friesinJarCount == 10)
            {
                ShakeManager.Instance.hasFlavor = true;
                dragScript.isDragging = false;
                StartCoroutine(PlayPourAnimation());
                Debug.Log("BBQ added");
            }

            else if (flavor == Flavor.Cheese && !ShakeManager.Instance.hasFlavor && ShakeManager.Instance.friesinJarCount == 10)
            {
                ShakeManager.Instance.hasFlavor = true;
                dragScript.isDragging = false;
                StartCoroutine(PlayPourAnimation());
                Debug.Log("Cheese added");
            }

        }
    }
    private IEnumerator PlayPourAnimation()
    {
        anim.enabled = true;

        anim.SetBool("isPour", true);

        yield return new WaitForSeconds(1.4f);


        dragScript.enabled = true;

        anim.SetBool("isPour", false);

    }

    void AddFriesToInventory()
    {
        foreach(var item in ShakeManager.Instance.friesInJar)
        {

            if (flavor == Flavor.SourCream)
            {
                InventoryManager.Instance.AddItem(item.friesData.SourCreamFries, 1);
            }
            else if (flavor == Flavor.BBQ)
            {
                InventoryManager.Instance.AddItem(item.friesData.BBQFries);
            }
            else if (flavor == Flavor.Cheese)
            {
                InventoryManager.Instance.AddItem(item.friesData.SourCreamFries);
            }
        }
    }
}

