using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public PuzleManager puzleManager;
    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("CorrectAnswer");
            puzleManager.correct();
        }
        else
        {
            Debug.Log("Paila");
        }
    }
}
