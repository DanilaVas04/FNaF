using System;
using UnityEngine;

public class Switch_Camera : MonoBehaviour
{
    public Material Gray_Material;
    public Material Green_Material;
    public AudioSource SoundCamera;
    public Camera[] cameras;
    public GameObject[] buttons;

    private void Start()
    {
        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }
        cameras[9].enabled = true;
    }

    void OnMouseDown()
    {
        ChangeObjectMaterial();
        ChangeCamera();
        SoundCamera.Play();
    }

    void ChangeObjectMaterial()
    {
        // Изменяем материал всех объектов на серый
        foreach (GameObject button in buttons)
        {
            Renderer buttonRenderer = button.GetComponent<Renderer>();
            if (buttonRenderer != null)
            {
                buttonRenderer.material = Gray_Material;
            }
        }

        // Изменяем материал нажатого объекта на зелёный
        Renderer currentRenderer = GetComponent<Renderer>();
        if (currentRenderer != null)
        {
            currentRenderer.material = Green_Material;
        }
    }

    void ChangeCamera()
    {
        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }
        int index = Array.IndexOf(buttons, gameObject);
        cameras[index].enabled = true;
    }
}
