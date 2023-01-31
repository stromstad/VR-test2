using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public int sceneBuildIndex;

    public void Switch()
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }
}