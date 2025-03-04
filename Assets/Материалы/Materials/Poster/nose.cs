using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public AudioSource SoundNose;
    void OnMouseDown()
    {
        SoundNose.Play();
    }
}
