using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject player;
    public bool death = false;

    private void Update()
    {
        if (death) 
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadSceneAsync("Menu"); 
        }
    }
}
