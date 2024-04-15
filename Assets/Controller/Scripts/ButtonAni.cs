using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAni : MonoBehaviour
{
    public Animator animator;
    private Button boton;
    public GameObject inventario;

    void Start()
    {

        boton = GetComponent<Button>();
        
        // Check if the Button component exists
        if (boton != null)
        {
            // If the Button component exists, add an onClick listener
            boton.onClick.AddListener(ReproducirAnimacion);
            boton.onClick.AddListener(inventario.GetComponent<Inventario>().Visible);
        }
        else
        {
            // Log an error if the Button component is not found
            Debug.LogError("Button component not found on GameObject: " + gameObject.name);
        }
    }

    void ReproducirAnimacion()
    {

        animator.SetTrigger("Sembrar");
    }
}

