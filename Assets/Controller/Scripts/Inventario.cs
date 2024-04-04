using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inv;
    public bool Activar_inv;
    public Text mensajeText; // Referencia al objeto de texto donde se mostrará el mensaje de la semilla

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Semilla"))
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                if (Bag[i].GetComponent<Image>().enabled == false)
                {
                    Bag[i].GetComponent<Image>().enabled = true;
                    Bag[i].GetComponent<Image>().sprite = collision.GetComponent<SpriteRenderer>().sprite;
                    MostrarMensajeSemilla("¡Has recogido una semilla!");
                    break;
                }
            }
        }
    }

    private void MostrarMensajeSemilla(string mensaje)
    {
        // Mostrar el mensaje en el objeto de texto
        mensajeText.text = mensaje;
        // Después de un tiempo, eliminar el mensaje
        StartCoroutine(EliminarMensaje());
    }

    private IEnumerator EliminarMensaje()
    {
        // Esperar unos segundos antes de eliminar el mensaje
        yield return new WaitForSeconds(3);
        mensajeText.text = ""; // Limpiar el texto después de unos segundos
    }

    // Método llamado cuando se presiona el botón de activar/desactivar inventario
    private void Update()
    {
        if (Activar_inv)
        {
            inv.SetActive(true);
        }
        else
        {
            inv.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Activar_inv = !Activar_inv;
        }
    }
}