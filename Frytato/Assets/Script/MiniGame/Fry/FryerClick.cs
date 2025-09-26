using Unity.VisualScripting;
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
        if (GameManager.Instance.gameState == GameState.Fry)
        {
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
}
