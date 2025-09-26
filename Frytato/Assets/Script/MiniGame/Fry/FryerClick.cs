using UnityEngine;

public class FryerClick : MonoBehaviour
{
    Animator anim;
    bool isCooking = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.gameState != GameState.Fry)
            return;

        // Get info about the current animation
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // Prevent changes if animation is still playing
        if (stateInfo.normalizedTime < 1f && stateInfo.IsName("Cook") || stateInfo.IsName("Uncook"))
            return;

        // Play animations only if the previous one is finished
        if (!isCooking)
        {
            anim.Play("Cook");
        }
        else
        {
            anim.Play("Uncook");
        }

        isCooking = !isCooking;
    }
}
