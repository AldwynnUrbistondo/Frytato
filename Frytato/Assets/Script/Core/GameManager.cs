using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState gameState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameState != GameState.Roam)
        {
            gameState = GameState.Roam;
            CameraManager.Instance.ChangeCamera(CameraType.MainCamera);
        }
    }
}

public enum GameState
{
    Roam,
    Slice,
    Fry,
    Shake
}
