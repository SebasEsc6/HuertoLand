using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inv;
    public bool Activar_inv;

    public void AgregarElemento(GameObject item)
    {
        // Agregar el elemento al inventario
        Bag.Add(item);
        ActualizarInventario();
    }

    public void ActualizarInventario()
    {
        // Iterar sobre los botones de inventario y asignarles el sprite correspondiente
        for (int i = 0; i < Bag.Count; i++)
        {
            Button boton = inv.transform.GetChild(i).GetComponent<Button>();
            boton.image.enabled = true;
            boton.image.sprite = Bag[i].GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Método llamado cuando se presiona el botón de activar/desactivar inventario
    public void AlternarInventario()
    {
        Activar_inv = !Activar_inv;
        inv.SetActive(Activar_inv);
    }
}