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

    private void Start()
    {
        generateQuestion();
    }
    public void correct()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
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
        // Seleccionar una pregunta aleatoria
        currentQuestion = Random.Range(0, QnA.Count);

        // Obtener el sprite de la pregunta seleccionada
        Sprite questionSprite = QnA[currentQuestion].QuestionImg;

        // Asignar el sprite al componente Image para mostrar la imagen
        QuestionImg.sprite = questionSprite;

        SetAnswers();
    }
}