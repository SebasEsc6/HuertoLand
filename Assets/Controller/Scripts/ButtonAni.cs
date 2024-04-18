using UnityEngine;
using UnityEngine.UI;

public class ButtonAni : MonoBehaviour
{
    public Animator animator;
    private Button boton;
    public GameObject inventario;
    public AudioClip sonido;

    void Start()
    {
        boton = GetComponent<Button>();

        if (boton != null)
        {
            boton.onClick.AddListener(ReproducirAnimacion);
            boton.onClick.AddListener(ReproducirSonido);
            boton.onClick.AddListener(() => inventario.GetComponent<Inventario>().Visible());
        }
        else
        {
            Debug.LogError("Button component not found on GameObject: " + gameObject.name);
        }
    }

    void ReproducirAnimacion()
    {
        animator.SetTrigger("Sembrar");
    }

    void ReproducirSonido()
    {
        if (sonido != null)
        {
            AudioSource.PlayClipAtPoint(sonido, Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("AudioClip not assigned to ButtonAni script.");
        }
    }
}