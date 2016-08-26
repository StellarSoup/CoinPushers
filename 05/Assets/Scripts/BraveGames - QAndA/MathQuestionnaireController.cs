using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MathQuestionnaireController : BG_QAndA_Menu {

    public GameObject Question;
    public GameObject Answers;

    private bool playerHasWon;

    private string[] mathQAndA;


	// Use this for initialization
	void Start () {

        playerHasWon = false;

        int answerIndex;
        mathQAndA = GenerateNewQuestionsAndAnswers(Answers.transform.childCount);

        answerIndex = (int)(Random.value * Answers.transform.childCount);
        DisplayQuestion(mathQAndA[answerIndex]);


        for (int i = 0; i < Answers.transform.childCount; i++)
        {
            DisplayAnswer(mathQAndA[i], i);
        }
        DisplayAnswer(mathQAndA[answerIndex], answerIndex);
	}

    //When the button is pressed the number displayed is compared against the question and
    //the player wins the game if it is correct
    public void CheckIfCorrectAnswer()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        Text buttonText = button.transform.GetChild(0).GetComponent<Text>();

        Text questionText = Question.transform.GetChild(0).GetComponent<Text>();
        string[] questionNumbers = questionText.text.Split('+');
        int value = int.Parse(questionNumbers[0]) + int.Parse(questionNumbers[1]);

        if (!playerHasWon)
        {
            playerHasWon = true;
            if (value == int.Parse(buttonText.text))
            {
                WinGame();
            }
            else
            {
                Lose();
            }
        }
    }

    //Displays an answer on a designated button
    private void DisplayAnswer(string equation,int index)
    {
        string[] array = equation.Split('=');
        string newAnswer = array[1];

        GameObject TextObject = Answers.transform.GetChild(index).GetChild(0).gameObject;
        Text txt = TextObject.GetComponent<Text>();
        txt.text = newAnswer;        
    }
    
    //Generate a string of questions and answers
    private string[] GenerateNewQuestionsAndAnswers(int arraySize)
    {
        string[] newAnswers = new string[arraySize];
        for(int i = 0; i < newAnswers.Length; i++)
        {
            //Create a new equation with the values inputed and an answer given
            string stringAnswer;
            int a, b, answer;
            a = (int)(Random.value * 15);
            b = (int)(Random.value * 15);
            answer = a + b;
            stringAnswer = a + "+" + b + "=" + answer;

            //Checks if the new answer given does not match any of the old answers
            bool newAnswer = true;
            for(int j = 0; j < i; j++)
            {
                int oldAnswer;
                string[] oldValues = newAnswers[j].Split('+','=');
                oldAnswer = int.Parse(oldValues[0]) + int.Parse(oldValues[1]);

                if(answer == oldAnswer)
                {
                    newAnswer = false;
                }
            }

            //if a new answer was given add it to the list
            if (newAnswer)
            {
                newAnswers[i] = stringAnswer;
            //else restart process
            }else
            {
                i--;
            }
            
        }
        return newAnswers;
    }

    //Set the new Question
    private void DisplayQuestion(string equation)
    {
        string[] array = equation.Split('=');
        string newQuestion = array[0];

        //Get the text component of the question UI
        Text questionText = Question.transform.GetChild(0).GetComponent<Text>();
        //Sets the new Question
        questionText.text = newQuestion;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
