using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogo : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.1f;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = string.Empty;
        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // Cambio de KeyCode.Space a KeyCode.V
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Panel"))
        {
            StartDialogue();
        }
    }
}



