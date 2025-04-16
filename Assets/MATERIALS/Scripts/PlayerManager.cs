using MyGameDevTools.SceneLoading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private float timerDuration = 4f;
    private float timeElapsed = 0f;
    private int random;
    public KeyCode exitMenu = KeyCode.Escape;
    public GameObject goldenFreddy;
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
                //Cursor.lockState = CursorLockMode.Confined;
                MySceneManager.TransitionAsync("MainMenu");
            } 
        }
        if (Input.GetKey(exitMenu)) 
        {
            //Cursor.lockState = CursorLockMode.Confined;
            MySceneManager.TransitionAsync("MainMenu");
        }
    }

    private void Start()
    {
        random = Random.Range(0, 101);
        if (random <= 5) { goldenFreddy.SetActive(true); }
    }
}
