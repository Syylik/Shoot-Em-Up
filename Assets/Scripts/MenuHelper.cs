using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHelper : MonoBehaviour
{
    public void ToScene(string name)
    {
        SceneFade.LoadScene(name);
        Time.timeScale = 1f;
    }

    public void ToScene(int buildIndex)
    {
        SceneFade.LoadScene(buildIndex);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneFade.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Leave() => Application.Quit();
}
