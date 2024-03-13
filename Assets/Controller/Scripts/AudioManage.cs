using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip errorSound;
    public AudioClip successSound;

    // Método para reproducir el sonido de error
    public void PlayErrorSound()
    {
        if (audioSource != null && errorSound != null)
        {
            audioSource.clip = errorSound;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Error: AudioSource o AudioClip no asignado.");
        }
    }

    // Método para reproducir el sonido de éxito al obtener la primera semilla
    public void PlaySuccessSound()
    {
        if (audioSource != null && successSound != null)
        {
            audioSource.clip = successSound;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Error: AudioSource o AudioClip no asignado.");
        }
    }
}
