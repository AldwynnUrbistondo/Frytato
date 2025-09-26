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


        switch (GameManager.Instance.platform)
        {
            case Platform.PC:
                movementx = Input.GetAxis("Horizontal");
                movementz = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(movementx * speed, 0.0f, movementz * speed);
                rb.linearVelocity = movement;
                break;

            case Platform.Mobile:
                Vector3 movementMobile = new Vector3(joystick.Horizontal * speed, 0.0f, joystick.Vertical * speed);
                rb.linearVelocity = movementMobile;
                break;

        }
        
    }
}
