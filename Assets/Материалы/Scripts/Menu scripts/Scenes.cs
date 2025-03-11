using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenes : MonoBehaviour
{
    public Text Mob1, Mob2, Mob3, Mob4;
    public Toggle EndlessEnergy;
    public string newSceneName;
    public static int Freddy, Bonnie, Chica, Foxy;
    public static bool isEnergy;

    public void ChangeScenes()
    {
        Freddy = int.Parse(Mob1.text);
        Bonnie = int.Parse(Mob2.text);
        Chica = int.Parse(Mob3.text);
        Foxy = int.Parse(Mob4.text);
        isEnergy = EndlessEnergy.isOn;
        SceneManager.LoadSceneAsync(newSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}
