using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLoading : MonoBehaviour
{
    public static GoToLoading Instance;
    public string sceneName;

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

    public void ChangeScene(string sceneName)
    {
        this.sceneName = sceneName;
        SceneManager.LoadScene("Loading");
    }
}
