using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Number : MonoBehaviour
{
    public Button Up;
    public Button Down;
    public Text label;
    void Start()
    {
        // ���������, ��� ������ ���������
        if (Up != null && Down != null)
        {
            // ��������� ��������� ��� ������� onClick
            Up.onClick.AddListener(OnButtonUpClick);
            Down.onClick.AddListener(OnButtonDownClick);
        }
    }

    // �����, ������� ����� ���������� ��� ������� �� ������
    void OnButtonUpClick()
    {
        if (label != null)
        {
            if (int.Parse(label.text) < 20)
            {
                label.text = (int.Parse(label.text) + 1).ToString();
            }
        }
    }
    void OnButtonDownClick()
    {
        if (label != null)
        {
            if (int.Parse(label.text) > 0)
            {
                label.text = (int.Parse(label.text) - 1).ToString();
            }
        }
    }
}
