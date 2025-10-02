using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState gameState;
    public Platform platform;

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
            BackToRoamButton();
        }
    }

    public void BackToRoamButton()
    {
        gameState = GameState.Roam;
        CameraManager.Instance.ChangeCamera(CameraType.MainCamera);
        UIManager.Instance.UpdateUI();
    }
}

public enum GameState
{
    Roam,
    Slice,
    Fry,
    Shake,
    Order
}

public enum Platform
{
    PC,
    Mobile
}
