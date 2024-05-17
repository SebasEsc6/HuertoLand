using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public PuzleManager puzleManager;
    private Button button;
    private Color originalColor;

    void Start()
    {
        button = GetComponent<Button>();
        originalColor = button.GetComponent<Image>().color;
    }

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("CorrectAnswer");
            button.GetComponent<Image>().color = Color.green;
            puzleManager.correct();
        }
        else
        {
            Debug.Log("Paila");
            button.GetComponent<Image>().color = Color.red; 
        }
        StartCoroutine(ResetColorAfterDelay(1f));
    }

    private IEnumerator ResetColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        button.GetComponent<Image>().color = originalColor;
    }
}