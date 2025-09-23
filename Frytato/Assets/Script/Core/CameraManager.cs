using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField] Camera mainCamera;
    [SerializeField] Camera sliceCamera;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ChangeCamera(CameraType camera)
    {
        if (camera == CameraType.MainCamera)
        {
            mainCamera.gameObject.SetActive(true);
            sliceCamera.gameObject.SetActive(false);
        }
        else if (camera == CameraType.SliceCamera)
        {
            mainCamera.gameObject.SetActive(false);
            sliceCamera.gameObject.SetActive(true);
        }
        else if (camera == CameraType.FryCamera)
        {

        }
        else if (camera == CameraType.ShakeCamera)
        {
            
        }
    }
}

public enum CameraType
{
    MainCamera,
    SliceCamera,
    FryCamera,
    ShakeCamera
}


