using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Agregar esta línea para acceder a la clase Image

public class Inventario : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inv;
    public bool Activar_inv;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("semilla"))
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                if (Bag[i].GetComponent<Image>().enabled == false)
                {
                    Bag[i].GetComponent<Image>().enabled = true;
                    Bag[i].GetComponent<Image>().sprite = other.GetComponent<SpriteRenderer>().sprite;
                    break;
                }
            }
        }
    }

    void Update()
    {
        if (Activar_inv)
        {
            inv.SetActive(true);
        }
        else
        {
            inv.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Activar_inv = !Activar_inv;
        }
    }
}