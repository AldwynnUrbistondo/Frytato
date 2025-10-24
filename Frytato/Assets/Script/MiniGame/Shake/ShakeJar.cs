using UnityEngine;

public class ShakeJar : MonoBehaviour
{
    [Header("Shake Settings")]
    public float moveAmount = 0.3f;   // How far up/down it moves
    public float moveSpeed = 8f;      // How fast it shakes
    public float returnSpeed = 5f;    // How fast it returns when stopped
    public Vector3 rotationFix = new Vector3(-90f, 0f, 0f); // Keep upright
    public bool canShake = false;     // Only true inside shake station

    private Vector3 startPos;         // Remember original position
    private bool movingUp = true;     // Direction toggle

    private float finishTime;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (ShakeManager.Instance.friesinJarCount == 10)
        {
            canShake = true;
            JarShake();
        }
    }

    public void JarShake()
    {
        finishTime += Time.deltaTime;

        transform.rotation = Quaternion.Euler(rotationFix);

        if (!canShake)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * returnSpeed);
            return;
        }

        if (Input.GetMouseButton(0)) // Hold to shake
        {
            Vector3 pos = transform.position;

            // Move up/down
            if (movingUp)
                pos.y += moveSpeed * Time.deltaTime;
            else
                pos.y -= moveSpeed * Time.deltaTime;

            // Clamp limits
            if (pos.y < startPos.y)
            {
                pos.y = startPos.y;
                movingUp = true;
            }

            if (pos.y > startPos.y + moveAmount)
            {
                pos.y = startPos.y + moveAmount;
                movingUp = false;
            }

            transform.position = pos;
        }
        else
        {
            // When player stops shaking, move back to start
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * returnSpeed);
        }

        if (finishTime > 5)
        {
            Debug.Log("Finished shaking!");
        }
    }

    public void SetCanShake(bool value)
    {
        canShake = value;
    }
}
