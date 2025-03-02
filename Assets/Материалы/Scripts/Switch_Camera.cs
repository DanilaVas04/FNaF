using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Switch_Camera : MonoBehaviour
{
    public Material Gray_Material;
    public Material Green_Material;
    public AudioSource SoundCamera;
    public Camera[] cameras;
    public GameObject[] buttons;

    void OnMouseDown()
    {
        ChangeObjectMaterial();
        ChangeCamera();
        SoundCamera.Play();
    }

    void ChangeObjectMaterial()
    {
        // �������� �������� ���� �������� �� �����
        foreach (GameObject button in buttons)
        {
            Renderer buttonRenderer = button.GetComponent<Renderer>();
            if (buttonRenderer != null)
            {
                buttonRenderer.material = Gray_Material;
            }
        }

        // �������� �������� �������� ������� �� ������
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
        Debug.Log(index);
        cameras[index].enabled = true;
    }
}
