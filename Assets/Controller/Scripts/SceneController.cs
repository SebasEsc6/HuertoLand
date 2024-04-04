using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject gameObject;

    public void Volver()
    {
        gameObject.SetActive(false);
    }
    public void IrMenu()
    {
        SceneManager.LoadScene("S_Menu");
        Debug.Log("VA AL MENU");
    }

 
    public void Cerrar()
    {
        //Cerrar
        Debug.Log("CIERRA");
    }

    public void Visible()
    {
        gameObject.SetActive(true);
    }

    public void ResetScene()
    {
        Destroy(this);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("RESET");
    }
    public void IrMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("VA Al MAIN MENU");
    }
    public void IrTutorial()
    {
        SceneManager.LoadScene("NivelTutorial");
        Debug.Log("Vamos al nivel tutorial");
    }
    public void IrPanelnicio()
    {
        SceneManager.LoadScene("Panelnicio");
        Debug.Log("Vamos al nivel tutorial");
    }

}
