using UnityEngine;
public class BillboardFX : MonoBehaviour
{
    public Camera targetCamera;
    public bool onUpdate = false;

    void Start()
    {
        FaceCamera();
    }

    void LateUpdate()
    {
        if (!onUpdate)
            return;
        FaceCamera();
    }

    void FaceCamera()
    {

        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
                        targetCamera.transform.rotation * Vector3.up);
    }
}