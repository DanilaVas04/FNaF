using MyGameDevTools.SceneLoading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private float timerDuration = 4f;
    private float timeElapsed = 0f;
    private int random;
    public KeyCode exitMenu = KeyCode.P;
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
                Cursor.lockState = CursorLockMode.None;
                MySceneManager.TransitionAsync("MainMenu");
            } 
        }
        if (Input.GetKey(exitMenu)) 
        {
            Cursor.lockState = CursorLockMode.None;
            MySceneManager.TransitionAsync("MainMenu");
        }

        // Проверяем нажатие на экран (левая кнопка мыши или касание)
        if (Input.GetMouseButtonDown(0))
        {
            // Скрываем курсор
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Проверяем нажатие клавиши "H"
        if (Input.GetKeyDown(KeyCode.H))
        {
            // Показываем курсор
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Start()
    {
        
        random = Random.Range(0, 101);
        if (random <= 5) { goldenFreddy.SetActive(true); }
    }

    public void ExitGame()
    {
        Cursor.lockState = CursorLockMode.None;
        MySceneManager.TransitionAsync("MainMenu");
    }
}
