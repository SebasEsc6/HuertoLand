using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public GameObject inv;
    public bool activar_inv;
    public GameObject panel;
    public bool activar_panel;

    public void Visible()
    {
        if (activar_inv)
        {
            inv.SetActive(false);
            activar_inv = false;
            panel.SetActive(false);
            activar_panel = false;
        }
        else 
        {
            inv.SetActive(true);
            activar_inv = true;
            panel.SetActive(true);
            activar_panel = true;
        }
    }
}