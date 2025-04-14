using UnityEngine;

public class Switch_Light : MonoBehaviour
{
    public bool isPressed = false;
    public Material Gray_Material;
    public Material Light_Material;
    public Light spotLight;
    public float minFlickerInterval = 0.1f; // Минимальный интервал между морганиями
    public float maxFlickerInterval = 0.5f; // Максимальный интервал между морганиями
    private float timer;
    private float flickerInterval;
    public AudioSource SoundLight;

    void Start()
    {
        ChangeObjectMaterial(Gray_Material);
        TurnOffLight();
    }

    void OnMouseDown()
    {
        if (isPressed)
        {
            isPressed = false;
            ChangeObjectMaterial(Gray_Material);
            TurnOffLight();    
        }
        else
        {
            isPressed = true;
            ChangeObjectMaterial(Light_Material);
            TurnOnLight();
        }
    }

    void ChangeObjectMaterial(Material material)
    {
        // Получаем компонент Renderer объекта
        Renderer renderer = GetComponent<Renderer>();

        // Проверяем, что компонент Renderer существует
        if (renderer != null)
        {
            // Меняем материал объекта
            renderer.material = material;
        }
    }

    void TurnOnLight()
    {
        if (spotLight != null)
        {
            spotLight.enabled = true;
            SoundLight.Play();
        }
    }

    void TurnOffLight()
    {
        if (spotLight != null)
        {
            spotLight.enabled = false;
            SoundLight.Stop();
        }
    }

    void Update()
    {
        if (isPressed)
        {
            // Уменьшаем таймер
            timer -= Time.deltaTime;

            // Если таймер истек, переключаем свет
            if (timer <= 0)
            {
                ToggleLight();
                SetRandomFlickerInterval();
            }
        }
    }
    void ToggleLight()
    {
        if (spotLight != null)
        {
            spotLight.enabled = !spotLight.enabled;
        }
    }

    void SetRandomFlickerInterval()
    {
        // Устанавливаем случайный интервал для следующего моргания
        flickerInterval = Random.Range(minFlickerInterval, maxFlickerInterval);
        timer = flickerInterval;
    }
    
    public void StopSoundPlaying()
    {
        SoundLight.Stop();
        spotLight.enabled = false;
    }
}
