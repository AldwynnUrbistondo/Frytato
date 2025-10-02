using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public Camera activeCamera;
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera sliceCamera;
    [SerializeField] Camera fryCamera;
    [SerializeField] Camera orderCamera;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        activeCamera = mainCamera;
    }

    public void ChangeCamera(CameraType camera)
    {
        if (camera == CameraType.MainCamera)
        {
            activeCamera = mainCamera;
            mainCamera.gameObject.SetActive(true);
            sliceCamera.gameObject.SetActive(false);
            fryCamera.gameObject.SetActive(false);
            orderCamera.gameObject.SetActive(false);
        }
        else if (camera == CameraType.SliceCamera)
        {
            activeCamera = sliceCamera;
            mainCamera.gameObject.SetActive(false);
            sliceCamera.gameObject.SetActive(true);
        }
        else if (camera == CameraType.FryCamera)
        {
            activeCamera = fryCamera;
            mainCamera.gameObject.SetActive(false);
            fryCamera.gameObject.SetActive(true);
        }
        else if (camera == CameraType.ShakeCamera)
        {
            
        }
        else if (camera == CameraType.OrderCamera)
        {
            activeCamera = orderCamera;
            mainCamera.gameObject.SetActive(false);
            orderCamera.gameObject.SetActive(true);
        }
    }
}

public enum CameraType
{
    MainCamera,
    SliceCamera,
    FryCamera,
    ShakeCamera,
    OrderCamera
}


