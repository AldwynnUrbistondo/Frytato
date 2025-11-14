using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContainerFries : MonoBehaviour
{

    public float disableDelay = 1.5f;
    private Animator anim;

    public List<GameObject> friesInContainer = new List<GameObject>();

    bool isAnimating = false;

    private void Start()
    {
       anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jar"))
        {
            ShakeJar jar = other.GetComponent<ShakeJar>();
            Animator jarAnimator = other.GetComponent<Animator>();
            ShakeJar shakeJar = other.GetComponent<ShakeJar>();

            if (jarAnimator != null && shakeJar != null)
            {
                // Disable flavor in ShakeManager
                ShakeManager.Instance.hasFlavor = false;

                // Enable the Animator
                jarAnimator.enabled = true;

                jar.finishedShaking = false;

                if (!isAnimating)
                    StartCoroutine(DisableAnimatorAndModifyJar(jarAnimator, shakeJar, disableDelay));
            }
        }
    }

    private IEnumerator DisableAnimatorAndModifyJar(Animator animator, ShakeJar shakeJar, float delay)
    {
        AudioManager.Instance.PlaySound(SoundType.Transfer);
        isAnimating = true;
        yield return new WaitForSeconds(delay);

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

            // Show fries BEFORE clearing the jar data
            ShowFries(shakeJar);

        }

        yield return new WaitForSeconds(0.5f);
        anim.Play("Change");
    }

    public void ShowFries(ShakeJar shakeJar)
    {
        foreach (GameObject fries in friesInContainer)
        {
            fries.SetActive(true);
            MeshRenderer friesMesh = fries.GetComponent<MeshRenderer>();

            if (ShakeManager.Instance.flavor == Flavor.SourCream)
            {
                friesMesh.materials[0].mainTexture = ShakeManager.Instance.friesInJar[0].friesData.SourCreamFries.cookTexture;
                friesMesh.materials[1] = ShakeManager.Instance.friesInJar[0].friesData.SourCreamFries.powderMaterial;
            }
            else if (ShakeManager.Instance.flavor == Flavor.BBQ)
            {
                friesMesh.materials[0].mainTexture = ShakeManager.Instance.friesInJar[0].friesData.BBQFries.cookTexture;
                friesMesh.materials[1] = ShakeManager.Instance.friesInJar[0].friesData.BBQFries.powderMaterial;
            }
            else if (ShakeManager.Instance.flavor == Flavor.Cheese)
            {
                friesMesh.materials[0].mainTexture = ShakeManager.Instance.friesInJar[0].friesData.CheeseFries.cookTexture;
                friesMesh.materials[1] = ShakeManager.Instance.friesInJar[0].friesData.CheeseFries.powderMaterial;
            }

        }

        shakeJar.PlaceToContainer();
        isAnimating = false;
    }

    public void HideFries()
    {
        foreach (GameObject fries in friesInContainer)
        {
            fries.SetActive(false);
        }
    }
}

