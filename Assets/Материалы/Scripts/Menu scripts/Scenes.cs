using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenes : MonoBehaviour
{
    public Text Mob1, Mob2, Mob3, Mob4;
    public Toggle EndlessEnergy;
    public string newSceneName;

    public void ChangeScenes()
    {
        SceneManager.LoadSceneAsync(newSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}
