using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzleManager : MonoBehaviour
{
    public List<Question> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public Image QuestionImg; // Debe ser de tipo Image para mostrar sprites
    public GameObject CompletionPanel; // Referencia al panel de completado
    public GameObject targetCanvas; // Referencia al canvas que quieres cerrar

    private void Start()
    {
        generateQuestion();
        CompletionPanel.SetActive(false); // Asegúrate de que el panel esté desactivado al inicio
    }
    public void correct()
    {
        if (QnA.Count > 0)
        {
            QnA.RemoveAt(currentQuestion);
            if (QnA.Count > 0)
            {
                generateQuestion();
            }
            else
            {
                Debug.Log("Todas las preguntas han sido respondidas.");
                StartCoroutine(ShowCompletionPanelTemporarily());
            }
        }
    }

    IEnumerator ShowCompletionPanelTemporarily()
    {
        CompletionPanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        CompletionPanel.SetActive(false);
        CloseCanvas();
    }
    void CloseCanvas()
    {
        targetCanvas.SetActive(false);
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            // Obtener el componente Image de la opción actual
            Image imageComponent = options[i].GetComponent<Image>();

            // Asignar sprite de la respuesta
            imageComponent.sprite = QnA[currentQuestion].Answer[i];

            // Comprobar y marcar la respuesta correcta
            if (QnA[currentQuestion].CorrectAnswer - 1 == i) // Restamos 1 para ajustarnos al índice base 0
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
            else
            {
                options[i].GetComponent<AnswerScript>().isCorrect = false;
            }
        }
    }

    void generateQuestion()
    {
        currentQuestion = Random.Range(0, QnA.Count);

        Sprite questionSprite = QnA[currentQuestion].QuestionImg;

        QuestionImg.sprite = questionSprite;

        SetAnswers();
    }
}