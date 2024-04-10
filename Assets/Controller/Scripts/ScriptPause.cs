using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPause : MonoBehaviour
{
    private bool juegoPausado = false; // Variable para rastrear si el juego está pausado o no
    [SerializeField] private GameObject menuinv;
    [SerializeField] private GameObject botoninv;

    public void TogglePausa()
    {
        // Alternar entre pausar y reanudar el juego
        juegoPausado = !juegoPausado;
        if (juegoPausado)
        {
            Time.timeScale = 0f; // Pausar el juego
        }
        else
        {
            Time.timeScale = 1f; // Reanudar el juego
        }
    }
    public void ToggleMochila()
    {
        bool estadoActual = menuinv.activeSelf;
        menuinv.SetActive(!estadoActual);

        botoninv.SetActive(true);
    }
}