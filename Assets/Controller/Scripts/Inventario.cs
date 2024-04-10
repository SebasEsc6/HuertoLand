using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inv;
    public bool activar_inv;

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
                    break;
                }
            }
        }
    }
}