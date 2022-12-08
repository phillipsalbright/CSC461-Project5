using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Challenge(int index)
    {
        BrickManager.approachingBricks = true;
        LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
