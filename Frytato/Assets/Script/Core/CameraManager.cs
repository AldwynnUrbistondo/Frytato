using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField] Camera mainCamera;
    [SerializeField] Camera washCamera;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ChangeCamera(CameraType camera)
    {
        if (camera == CameraType.MainCamera)
        {
            mainCamera.enabled = true;
            washCamera.enabled = false;
        }
        else if(camera == CameraType.WashCamera)
        {
            mainCamera.enabled = false;
            washCamera.enabled = true;
        }
    }
}

public enum CameraType
{
    MainCamera,
    WashCamera,
    SliceCamera,
    FryCamera,
    ShakeCamera
}


