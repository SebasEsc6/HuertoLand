using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    // Nombre de la escena a la que queremos cambiar
    public string nombreDeEscena;

    // Método que se llama cuando un objeto colisiona con este objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos si el objeto que colisionó tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Quitar el comentario!");
            // Cambiamos a la escena deseada
            //SceneManager.LoadScene(nombreDeEscena);
        }
    }
}