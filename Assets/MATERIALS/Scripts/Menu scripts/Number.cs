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
        // Проверяем, что кнопка назначена
        if (Up != null && Down != null)
        {
            // Добавляем слушатель для события onClick
            Up.onClick.AddListener(OnButtonUpClick);
            Down.onClick.AddListener(OnButtonDownClick);
        }
    }

    // Метод, который будет вызываться при нажатии на кнопку
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
