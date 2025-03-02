using UnityEngine;

public class Switch_Light : MonoBehaviour
{
    public bool isPressed = false;
    public Material Gray_Material;
    public Material Light_Material;
    public Light spotLight;
    public float minFlickerInterval = 0.1f; // ����������� �������� ����� ����������
    public float maxFlickerInterval = 0.5f; // ������������ �������� ����� ����������
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
        // �������� ��������� Renderer �������
        Renderer renderer = GetComponent<Renderer>();

        // ���������, ��� ��������� Renderer ����������
        if (renderer != null)
        {
            // ������ �������� �������
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
            // ��������� ������
            timer -= Time.deltaTime;

            // ���� ������ �����, ����������� ����
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
        // ������������� ��������� �������� ��� ���������� ��������
        flickerInterval = Random.Range(minFlickerInterval, maxFlickerInterval);
        timer = flickerInterval;
    }
    
    public void StopSoundPlaying()
    {
        SoundLight.Stop();
        spotLight.enabled = false;
    }
}
