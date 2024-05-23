using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI tiempoText;
    private float tiempoInicio;

    void Start()
    {
        tiempoInicio = Time.time;
    }

    void Update()
    {
        float tiempoTranscurrido = Time.time - tiempoInicio;
        string minutos = ((int)tiempoTranscurrido / 60).ToString("00");
        string segundos = (tiempoTranscurrido % 60).ToString("00");
        tiempoText.text = minutos + ":" + segundos;
    }
}
