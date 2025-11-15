using UnityEngine;
public class BillboardFX : MonoBehaviour
{
    public Camera targetCamera;
    public CameraType cameraType;
    public bool onUpdate = false;

    void Start()
    {
        SetTargetCamera();
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

    void SetTargetCamera()
    {
        switch(cameraType)
        {
            case CameraType.MainCamera:
                targetCamera = CameraManager.Instance.mainCamera;
                break;
            case CameraType.SliceCamera:
                targetCamera = CameraManager.Instance.sliceCamera;
                break;
            case CameraType.FryCamera:
                targetCamera = CameraManager.Instance.fryCamera;
                break;
            case CameraType.ShakeCamera:
                targetCamera = CameraManager.Instance.shakeCamera;
                break;
        }
    }
}