using UnityEngine;

public class Switch_Door : MonoBehaviour
{
    public bool isPressed = false;
    public Material Red_Material;
    public Material Green_Material;
    public GameObject objectToMove;
    public Vector3 targetPositionUp;
    public Vector3 targetPositionDown;
    public float speed = 1.0f;
    public AudioSource SoundDoor;

    void Start()
    {
        ChangeObjectMaterial(Red_Material);
    }

    void OnMouseDown()
    {
        if (isPressed)
        {
            isPressed = false;
            ChangeObjectMaterial(Red_Material);
            SoundDoor.Play();
        }
        else
        {
            isPressed = true;
            ChangeObjectMaterial(Green_Material);
            SoundDoor.Play();
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

    void Update()
    {
        if (!isPressed)
        {
            if (objectToMove != null)
            {
                objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, targetPositionUp, speed * Time.deltaTime);
            }
        }
        else
        {
            if (objectToMove != null)
            {
                objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, targetPositionDown, speed * Time.deltaTime);
            }
        }
    }
}
