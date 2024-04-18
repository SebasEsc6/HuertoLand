using ControladorSonidosprueba;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    [SerializeField] private AudioClip semilla;
    [SerializeField] private AudioClip Caida;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            ControladorSonidos.Instance.EjecutarSonido(semilla);
            //Destroy(gameObject);
        }
        else
        {
            ControladorSonidos.Instance.EjecutarSonido(Caida);
        }
    }
    
}
