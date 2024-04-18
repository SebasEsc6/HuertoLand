using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public string nombreDeEscena;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Quitar el comentario!");
            // Cambiamos a la escena deseada
            SceneManager.LoadScene(nombreDeEscena);
        }
    }
}