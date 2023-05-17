using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    private static int? sceneToLoadIndex;
    private static string sceneToLoadName;
    private static Animator anim;

    private void Awake() => anim = GetComponent<Animator>();

    public static void LoadScene(int loadScene)
    {
        anim.SetTrigger("fade");
        sceneToLoadIndex = loadScene;
    }

    public static void LoadScene(string loadScene)
    {
        anim.SetTrigger("fade");
        sceneToLoadName = loadScene;
    }
    public void LoadSceneAnim()
    {
        if(sceneToLoadIndex != null) SceneManager.LoadScene(sceneToLoadIndex.Value);
        else if(sceneToLoadName.Length > 0) SceneManager.LoadScene(sceneToLoadName);

        sceneToLoadIndex = null;
        sceneToLoadName = string.Empty;
    }
}
