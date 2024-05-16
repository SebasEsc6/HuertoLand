using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Image buttonImage;
    public Sprite newImage;
    public Canvas canvasToToggle;

    private bool canvasVisible = false;

    public void OnButtonClick()
    {
        // Cambiar la imagen del botón
        if (buttonImage != null && newImage != null)
        {
            buttonImage.sprite = newImage;
        }

        // Mostrar u ocultar el canvas
        if (canvasToToggle != null)
        {
            canvasVisible = !canvasVisible;
            canvasToToggle.gameObject.SetActive(canvasVisible);
        }
    }
}