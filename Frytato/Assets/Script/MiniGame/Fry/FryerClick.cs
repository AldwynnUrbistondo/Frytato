using UnityEngine;

public class FryerClick : MonoBehaviour
{
    Animator anim;
    public bool isCooking = false;
    public int capacity = 10;
    public int currentFries = 0;
    public bool canAddFries = true;

    [SerializeField] GameObject particle;

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

            if(currentFries != 0)
            {
                particle.SetActive(true);
                canAddFries = false;
            }
            
        }
        else
        {
            anim.Play("Uncook");
            particle.SetActive(false);
        }

        isCooking = !isCooking;
    }
}
