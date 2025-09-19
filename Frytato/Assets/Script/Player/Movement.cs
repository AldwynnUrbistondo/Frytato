using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;
    float movementx;
    float movementz;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance.gameState != GameState.Roam)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        movementx = Input.GetAxis("Horizontal");
        movementz = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movementx * speed, 0.0f, movementz * speed);
        rb.linearVelocity = movement;
    }
}
