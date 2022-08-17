using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnClickSceneButton(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
