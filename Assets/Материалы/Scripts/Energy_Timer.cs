using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Energy_Timer : MonoBehaviour
{
    private float speed = 20.0f; // Продолжительность таймера в секундах
    private float timer = 0;
    public TMP_Text energy;
    public string newSceneName;
    public GameObject leftButton,rightButton,leftButtonLight,rightButtonLight,Picture;
    private Switch_Door leftDoor, rightDoor;
    private Switch_Light leftLight, rightLight;
    private bool leftIs, rightIs;
    public AudioSource fan,endEnergy;
    public Light[] lights;
    private bool energyDownIs = true;
    private bool isEnergy = Scenes.isEnergy;

    private void Start()
    {
        leftDoor = leftButton.GetComponent<Switch_Door>();
        rightDoor = rightButton.GetComponent<Switch_Door>();
        leftLight = leftButtonLight.GetComponent<Switch_Light>();
        rightLight = rightButtonLight.GetComponent<Switch_Light>();
    }

    void Update()
    {
        if (energyDownIs && !isEnergy)
        {
            leftIs = leftDoor.isPressed;
            rightIs = rightDoor.isPressed;
            if (rightIs && leftIs) { speed = 4.0f; }
            else if (rightIs || leftIs) { speed = 8.0f; }
            else { speed = 20.0f; }

            if (int.Parse(energy.text) > 0)
            {
                // Увеличиваем таймер на время, прошедшее с последнего кадра
                timer += Time.deltaTime;
                // Проверяем, достиг ли таймер заданной продолжительности
                if (timer >= speed)
                {
                    timer = 0;
                    energy.text = (int.Parse(energy.text) - 1).ToString();
                }
            }
            else
            {
                leftDoor.isPressed = false;
                rightDoor.isPressed = false;
                leftLight.isPressed = false;
                rightLight.isPressed = false;
                leftLight.StopSoundPlaying();
                rightLight.StopSoundPlaying();
                fan.enabled = false;
                foreach (Light light in lights)
                {
                    light.enabled = false;
                }
                endEnergy.Play();
                Picture.SetActive(false);
                energyDownIs = false;
                timer = 0;
                speed = 0.5f;
            }
        }
        else if (!isEnergy)
        {
            timer += Time.deltaTime;
            if (timer >= speed)
            {
                leftButton.SetActive(false);
                rightButton.SetActive(false);
                leftButtonLight.SetActive(false);
                rightButtonLight.SetActive(false);
            }
        } 
    }
}
