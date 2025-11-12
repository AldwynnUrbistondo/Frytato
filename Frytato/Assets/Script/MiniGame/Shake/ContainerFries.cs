using UnityEngine;
using System.Collections;
public class ContainerFries : MonoBehaviour
{

    public float disableDelay = 1.5f;
    private Animator currentAnimator; // store the animator that entered

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jar"))
        {
            Animator jarAnimator = other.GetComponent<Animator>();
            ShakeJar shakeJar = other.GetComponent<ShakeJar>();

            if (jarAnimator != null && shakeJar != null)
            {
                // Disable flavor in ShakeManager
                ShakeManager.Instance.hasFlavor = false;

                // Enable the Animator
                jarAnimator.enabled = true;

                
                StartCoroutine(DisableAnimatorAndModifyJar(jarAnimator, shakeJar, disableDelay));
            }
        }
    }

    private IEnumerator DisableAnimatorAndModifyJar(Animator animator, ShakeJar shakeJar, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Disable Animator
        if (animator != null)
        {
            animator.enabled = false;
        }


        if (shakeJar.dragScript != null)
        {
            shakeJar.dragScript.enabled = false;
        }
       
        if (shakeJar != null)
        {
            shakeJar.rb.isKinematic = true;
            shakeJar.rb.useGravity = false;
        }
    }
}

