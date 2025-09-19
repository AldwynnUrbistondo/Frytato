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

        Instance = this;
    }

    public void ChangeCamera(CameraType camera)
    {
        if (camera == CameraType.MainCamera)
        {
            mainCamera.gameObject.SetActive(true);
            washCamera.gameObject.SetActive(false);
        }
        else if(camera == CameraType.WashCamera)
        {
            mainCamera.gameObject.SetActive(false);
            washCamera.gameObject.SetActive(true);
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


