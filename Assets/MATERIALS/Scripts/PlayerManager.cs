using MyGameDevTools.SceneLoading;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public GameObject[] uiElements;
    private bool isUIHidden = true;

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
        

        // Проверяем нажатие на экран (левая кнопка мыши или касание)
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Скрываем курсор, если кликнули на пустой экран
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        // Проверяем нажатие клавиши "H"
        if (Input.GetKeyDown(KeyCode.H))
        {
            // Показываем курсор
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // Проверяем нажатия на экран
        if (Input.GetMouseButtonDown(0) && isUIHidden)
        {
            ShowUI();
            isUIHidden = false;
            StartCoroutine(HideUIDelayed(5f));
        }
    }

    private void Start()
    {
        HideUI();
        random = Random.Range(0, 101);
        if (random <= 5) { goldenFreddy.SetActive(true); }
    }

    public void ExitGame()
    {
        Cursor.lockState = CursorLockMode.None;
        MySceneManager.TransitionAsync("MainMenu");
    }

    void HideUI()
    {
        foreach (var uiElement in uiElements)
        {
            uiElement.SetActive(false);
        }
    }

    void ShowUI()
    {
        foreach (var uiElement in uiElements)
        {
            uiElement.SetActive(true);
        }
    }

    IEnumerator HideUIDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideUI();
        isUIHidden = true;
    }
}
