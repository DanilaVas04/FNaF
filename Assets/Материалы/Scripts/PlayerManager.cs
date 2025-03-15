using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private float timerDuration = 4f;
    private float timeElapsed = 0f;
    public KeyCode exitMenu = KeyCode.Escape;
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject player;
    public bool death = false;

    private void Update()
    {
        if (death) 
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= timerDuration)
            {
                Cursor.lockState = CursorLockMode.Confined;
                SceneManager.LoadSceneAsync("MainMenu");
            } 
        }
        if (Input.GetKey(exitMenu)) 
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadSceneAsync("MainMenu"); 
        }
    }
}
