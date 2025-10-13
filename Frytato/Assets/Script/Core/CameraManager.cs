using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public Camera activeCamera;
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera sliceCamera;
    [SerializeField] Camera fryCamera;
    [SerializeField] Camera shakeCamera;
    //[SerializeField] Camera packCamera;
    [SerializeField] Camera orderCamera;
    //[SerializeField] Camera serveCamera;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        activeCamera = mainCamera;
        SetActiveCamera(mainCamera);
    }

    public void ChangeCamera(CameraType camera)
    {
        // Disable all cameras first
        DisableAllCameras();

        switch (camera)
        {
            case CameraType.MainCamera:
                activeCamera = mainCamera;
                mainCamera.gameObject.SetActive(true);
                break;

            case CameraType.SliceCamera:
                activeCamera = sliceCamera;
                sliceCamera.gameObject.SetActive(true);
                break;

            case CameraType.FryCamera:
                activeCamera = fryCamera;
                fryCamera.gameObject.SetActive(true);
                break;

            case CameraType.ShakeCamera:
                activeCamera = shakeCamera;
                shakeCamera.gameObject.SetActive(true);
                break;
            /*
            case CameraType.PackCamera:
                activeCamera = packCamera;
                packCamera.gameObject.SetActive(true);
                break;
            */
            case CameraType.OrderCamera:
                activeCamera = orderCamera;
                orderCamera.gameObject.SetActive(true);
                break;
            /*
            case CameraType.ServeCamera:
                activeCamera = serveCamera;
                serveCamera.gameObject.SetActive(true);
                break;
            */
        }
    }

    void DisableAllCameras()
    {
        mainCamera.gameObject.SetActive(false);
        sliceCamera.gameObject.SetActive(false);
        fryCamera.gameObject.SetActive(false);
        shakeCamera.gameObject.SetActive(false);
        //packCamera.gameObject.SetActive(false);
        orderCamera.gameObject.SetActive(false);
        //serveCamera.gameObject.SetActive(false);
    }

    void SetActiveCamera(Camera cam)
    {
        DisableAllCameras();
        cam.gameObject.SetActive(true);
        activeCamera = cam;
    }
}

public enum CameraType
{
    MainCamera,
    SliceCamera,
    FryCamera,
    ShakeCamera,
    PackCamera,
    OrderCamera,
    ServeCamera,
}
