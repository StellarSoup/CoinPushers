using UnityEngine;
using System.Collections;

public class QuestionControls : BG_QAndA_Menu
{


    //Stores the Question and the correct answer for it
    private string[,] QAndA;
    //Stores a series of incorrect answers
    private string[] wrongAnsers;

    //Gets the gameobjects that hold the questions and answers, and the wrong answers
    private GameObject Question;
    private GameObject Answer;
    private GameObject[] WrongAnswersObjects;

    //Check to see if the player has won
    private bool playerHasWon;


	struct QAIndex{
		public static int Question;
	}


	// Use this for initialization
	void Start () {
		WrongAnswersObjects = new GameObject[3];
		QAndA =  new string[5,2]{   { "What is Red?", "Apple" } , { "What is Blue?", "BlueBerry" } , {"2 + 2 =", "4"}, {"What has the most Salt?","The Sea" },
                                    {"Who am I?","Me" } };
        wrongAnsers = new string []{   "Hola Sr. sopa", "For Whom the Bell Toles", "This is Ground Control",
                                        "When?","Black Star","C'est le français","Can you here me Major Tom",
                                        "Tha mi Caimbeul","The Devil","Is in the details"};
        GenerateNewAnswersAndQuestion();
        playerHasWon = false;
    }

    void GenerateNewAnswersAndQuestion()
    {
        Question = GameObject.Find(gameObject.name + "/Question/Container/Text");
        Answer = GameObject.Find(gameObject.name + "/Answers");
        int QuestionIndex = (int)(Random.value * (QAndA.Length / 2));

        //Change the question
        TextMesh questMesh = Question.GetComponent<TextMesh>();
        questMesh.text = QAndA[QuestionIndex, 0];
        Question.name = QAndA[QuestionIndex, 0];
		QAIndex.Question = QuestionIndex;


        int[] anserChosen = { -1,-1,-1};
        int index = 0;

        //Fill the answers with incorrect answers
        while (index < Answer.transform.childCount)
        {
			//Gets the gameObject
			WrongAnswersObjects[index] = Answer.transform.GetChild(index).gameObject;

			//Collects a random word from the wronganswers bank
            int ran = (int)(Random.value * wrongAnsers.Length);
            bool newAnswer = true;

			//If the new word matches any of the words in the bank then it will not be used
            for (int j = 0; j < anserChosen.Length; j++)
            {
                if (ran == anserChosen[j])
                {
                    newAnswer = false;
                }
            }

			//If there is a new answer
            if (newAnswer)
            {
				//Add it to the list
				anserChosen [index] = ran;
				WrongAnswersObjects[index].name = wrongAnsers[ran];
				TextMesh ansMesh = WrongAnswersObjects[index].transform.GetChild(0).GetComponent<TextMesh>();
                ansMesh.text = wrongAnsers[ran];
                index++;
            }
            
        }

        
        //Insert randomly the correct answer
        int chooseChild = (int)(Random.value * 3);
        GameObject cchild = Answer.transform.GetChild(chooseChild).gameObject;
        cchild.name = QAndA[QuestionIndex, 1];
        TextMesh cansMesh = cchild.transform.GetChild(0).GetComponent<TextMesh>();
        cansMesh.text = QAndA[QuestionIndex, 1];
    }
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform.parent.name.Equals(QAndA[QAIndex.Question, 1]))
                {
                    if (!playerHasWon)
                    {
                        playerHasWon = true;
                        WinGame();
                    }
                }
                else
                {
                    Lose();
                }
            }

            
		}
	}
}
