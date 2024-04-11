using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public GameObject inv;
    public bool activar_inv;
    public void Visible()
    {
        if (activar_inv)
        {
            inv.SetActive(false);
            activar_inv = false;
        }
        else 
        {
            inv.SetActive(true);
            activar_inv = true;
        }
    }
}