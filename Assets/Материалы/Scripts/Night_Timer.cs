using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class Night_Timer : MonoBehaviour
{
    public float duration = 80.0f; // ����������������� ������� � ��������
    private float timer = 0;
    public TMP_Text time;
    public VideoPlayer videoPlayer;
    public GameObject winObject;
    public string nextSceneName;

    void Start()
    {
        // ������������� �� ������� ��������� ��������������� �����
        videoPlayer.loopPointReached += OnVideoFinished;
        AudioListener.pause = false;
    }

    void Update()
    {
        if (int.Parse(time.text) != 6)
        {
            // ����������� ������ �� �����, ��������� � ���������� �����
            timer += Time.deltaTime;
            // ���������, ������ �� ������ �������� �����������������
            if (timer >= duration)
            {
                timer = 0;
                if (time.text != "12") { time.text = (int.Parse(time.text) + 1).ToString(); }
                else { time.text = "1"; }
            }
        }
        else
        {
            NightComplete();
        }
    }

    void NightComplete()
    {
        winObject.SetActive(true);
        AudioListener.pause = true;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // ��������� ��������� ����� ����� ���������� �����
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadSceneAsync(nextSceneName);
    }

    void OnDisable()
    {
        // ������������ �� ������� ��� ����������� �������
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}
