using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;
    float movementx;
    float movementz;
    [SerializeField] FixedJoystick joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (GameManager.Instance.platform != Platform.Mobile)
        {
            joystick.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (GameManager.Instance.gameState != GameState.Roam)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        Vector3 movement = Vector3.zero;

        switch (GameManager.Instance.platform)
        {
            case Platform.PC:
                movementx = Input.GetAxis("Horizontal");
                movementz = Input.GetAxis("Vertical");

                movement = new Vector3(movementx, 0.0f, movementz) * speed;
                rb.linearVelocity = movement;
                break;

            case Platform.Mobile:
                movement = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical) * speed;
                rb.linearVelocity = movement;
                break;
        }

        // Rotate toward movement direction if moving
        if (movement.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
