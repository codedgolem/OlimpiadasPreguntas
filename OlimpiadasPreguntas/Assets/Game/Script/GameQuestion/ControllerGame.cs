
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class ControllerGame : MonoBehaviour
{

    public TextMeshProUGUI question;
    //Para multiple questions
    public TextMeshProUGUI option1;
    public TextMeshProUGUI option2;
    public TextMeshProUGUI option3;
    public TextMeshProUGUI option4;
    public TextMeshProUGUI description;
    public TextMeshProUGUI title;
    public TextMeshProUGUI difficulty;

    //Paneles contenedores principales
    public GameObject panelInicio;
    public GameObject panelJuego;


    //Para paneles de preguntas
    public GameObject panelOpen;
    public GameObject panelTrueFalse;
    public GameObject panelMultiple;
    public GameObject panelNextLevel;

    public GameObject buttonNext;

    //Para open questions
    public TextMeshProUGUI answerOpen;
    public TextMeshProUGUI versiculoOpen;

    //Lista de preguntas o variables inciales
    List<Question> list_questionsEasy = new List<Question>();
    List<Question> list_questionsHard = new List<Question>();
    private List<Question> currentList; // Lista que apunta a la dificultad actual
    private Question currentQuestion;
    private int index;
    private int lastIndexE = -1;
    private int lastIndexH = -1;
    private int numEasy;
    private int numHard;

    //Contador
    private int correctAnswers = 0;
    private bool stateAnswer;

    public AudioSource musicaFondo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        list_questionsEasy.Clear();
        list_questionsHard.Clear();
        LoadQuestionsMultiple("SELECCION_MULTIPLE_2024.txt");
        //  LoadQuestionsMultiple("ArchivoPreguntas.txt");
        LoadQuestionsTrueFalse("FALSO_VERDADERO_2024.txt");
        LoadQuestionsOpen("ABIERTAS_2024.txt");
        Debug.Log("Preguntas fáciles cargadas: " + list_questionsEasy.Count);
        Debug.Log("Preguntas difíciles cargadas: " + list_questionsHard.Count);
        

        panelInicio.SetActive(true);
        musicaFondo.Play();
        panelJuego.SetActive(false);
        panelNextLevel.SetActive(false);

        currentList = list_questionsEasy;

    }

    public void ClickStartGame()
    {
        panelInicio.SetActive(false);
        panelJuego.SetActive(true);

        ShowInScene(); // Inicio de Juego


    }

    
    void Update()
    {

    }

    
    public void ShowInScene()
    {
        stateAnswer = true;
        title.text = ""; 
        description.text = "";

        // Cambiado == por .Equals() para comparar la lista actual
        if (currentList.Equals(list_questionsEasy) && numEasy < list_questionsEasy.Count)
        {
            do {
                index = UnityEngine.Random.Range(0, list_questionsEasy.Count);
            } while (index.Equals(lastIndexE) || list_questionsEasy[index].Estado.Equals(true));

            lastIndexE = index;
            numEasy++;
            currentQuestion = list_questionsEasy[index];
            SetQuestionPanel();
        }
        else if (numHard < list_questionsHard.Count)
        {
            if (numHard.Equals(0))
            {
                currentList = list_questionsHard; // Cambiamos el puntero a la lista difícil
                panelNextLevel.SetActive(true);
                title.text = "<size=150%>¡Felicidades! Nivel fácil completado.<size=150%>";
                description.text = "Prepárate para poner a prueba tus conocimientos de la palabra de Dios con un nivel difícil.";
                
            }

            do {
                index = UnityEngine.Random.Range(0, list_questionsHard.Count);
            } while (index.Equals(lastIndexH) || list_questionsHard[index].Estado.Equals(true));

            lastIndexH = index; // si es igual al de la pregunta anterior le dice que vuelva a buscar otra pregunta al azar porque esa ya salio
                                //solo cuando el numero o el indice de la pregunta es diferente , este ciclo se rompe y el programa continua
            numHard++;
            currentQuestion = list_questionsHard[index]; // acceso por indice // tomara el indice de la pregunta que le pedimos arriba que buscara aleatoriamente y lo convertira en la pregunta actual  
            SetQuestionPanel();
        }
        else
        {
            GameOver();
        }
    }

    private void SetQuestionPanel()
    {
        if (currentQuestion is MultipleQuestion) // la palabra clave is se utiliza para que el programa entienda que tipo de pregunta tiene que prosesar o mostrar
                                                 // la palabra clave is actua como un clasificador para mandar cada pregunta al panel correcto 
            ShowMultipleQuestion((MultipleQuestion)currentQuestion);
        else if (currentQuestion is TrueFalseQuestion)
            ShowTrueFalseQuestion((TrueFalseQuestion)currentQuestion);
        else if (currentQuestion is AbiertasQuestion)
            ShowOpenQuestion((AbiertasQuestion)currentQuestion);
    }
    
 
    //METODOS DE CARGAR DATOS
    public void LoadQuestionsMultiple(String filename)
    {
        string route = Path.Combine(Application.streamingAssetsPath, filename);
        StreamReader info = new StreamReader(route);

        string line;

        while ((line = info.ReadLine()) != null)
        {
            //Debug.Log(line);

            string[] datos = line.Split('-');
            MultipleQuestion question = new MultipleQuestion(datos[0], datos[1], datos[2], datos[3], datos[4], datos[5], datos[6], datos[7]);

            if (datos[7].Equals("Facil"))
            {
                list_questionsEasy.Add(question);
            }
            else
            {
                list_questionsHard.Add(question);
            }
        }
        info.Close();
    }

    public void LoadQuestionsTrueFalse(String filename)
    {
        string route = Path.Combine(Application.streamingAssetsPath, filename);
        StreamReader info = new StreamReader(route);
        string line;
        while ((line = info.ReadLine()) != null)
        {
            //Debug.Log(line);
            string[] datos = line.Split('-');
            TrueFalseQuestion question = new TrueFalseQuestion(datos[0], bool.Parse(datos[1]), datos[2], datos[3]);
            if (datos[3].Equals("Facil"))
            {
                list_questionsEasy.Add(question);
            }
            else
            {
                list_questionsHard.Add(question);
            }

        }
        info.Close();
    }

    public void LoadQuestionsOpen(String filename)
    {
        string route = Path.Combine(Application.streamingAssetsPath, filename);
        StreamReader info = new StreamReader(route);
        string line;
        while ((line = info.ReadLine()) != null)
        {
            //Debug.Log(line);
            string[] datos = line.Split('-');
            AbiertasQuestion question = new AbiertasQuestion(datos[0], datos[1], datos[2], datos[3]);
            if (datos[3].Equals("Facil"))
            {
                list_questionsEasy.Add(question);
            }
            else
            {
                list_questionsHard.Add(question);
            }

        }
        info.Close();
    }

    //METODOS DE VERIFICACIONES
    public void MultipleAnswerVerify(int option)
    {
        MultipleQuestion multipleQ = (MultipleQuestion)currentQuestion;
        bool correct = false;

        switch (option)
        {
            case 1:
                correct = option1.text.Equals(multipleQ.Answer);
                break;
            case 2:
                correct = option2.text.Equals(multipleQ.Answer);
                break;
            case 3:
                correct = option3.text.Equals(multipleQ.Answer);
                break;
            case 4:
                correct = option4.text.Equals(multipleQ.Answer);
                break;
        }
        if (correct)
        {
            title.text = "Correcto";
            description.text = multipleQ.Versiculo;
            if (stateAnswer.Equals(true)) { correctAnswers++; stateAnswer = false; }
            {
                correctAnswers++;
                stateAnswer = false;
            }
        }
        else
        {
            title.text = "Incorrecto";
            description.text = "Intenta de nuevo";
        }
        
        panelNextLevel.SetActive(true);
    }
    public void TrueFalseAnswerVerify(bool option)
    {
        TrueFalseQuestion trueFalseQ = (TrueFalseQuestion)currentQuestion;
        if (option == trueFalseQ.Answer)
        {
            title.text = "Correcto";
            description.text = trueFalseQ.Versiculo;
            if (stateAnswer == true)
            {
                correctAnswers++;
                stateAnswer = false;
            }


        }
        else
        {
            title.text = "Incorrecto";
            description.text = "Intenta de nuevo";

        }
    }

    public void OpenAnswerVerify()
    {
        AbiertasQuestion openQ = (AbiertasQuestion)currentQuestion;
        answerOpen.text = openQ.Answer;
        versiculoOpen.text = openQ.Versiculo;
    }

    void GameOver()
    {
        panelNextLevel.SetActive(true);
        int total = list_questionsEasy.Count+ list_questionsHard.Count;
        title.text = "<size=150%>¡Felicidades, has completado el juego!<size=150%>";
        description.text = "Respuestas Correctas:\n " + correctAnswers + " de " + total;
        buttonNext.SetActive(false);

    }


    //METODOS SHOW DE MOSTRAR 

    public void ShowMultipleQuestion(MultipleQuestion mc)
    {
        panelTrueFalse.SetActive(false);
        panelOpen.SetActive(false);
        panelMultiple.SetActive(true);

        question.text = mc.Pregunta;
        option1.text = mc.Option1;
        option2.text = mc.Option2;
        option3.text = mc.Option3;
        option4.text = mc.Option4;
        difficulty.text = mc.Difficult;

        mc.Estado = true;

        Debug.Log("Pregunta mostrada: " + mc.Question);
    }

   

    public void ShowTrueFalseQuestion(TrueFalseQuestion tf)
    {
        panelMultiple.SetActive(false);
        panelOpen.SetActive(false);
        panelTrueFalse.SetActive(true);
        question.text = tf.Pregunta;
        difficulty.text = tf.Difficult;


        tf.Estado = true;
    }

    public void ShowOpenQuestion(AbiertasQuestion oq)
    {
        panelMultiple.SetActive(false);
        panelTrueFalse.SetActive(false);
        panelOpen.SetActive(true);
        question.text = oq.Pregunta;
        answerOpen.text = "";
        versiculoOpen.text = "";
        difficulty.text = oq.Difficult;

        oq.Estado = true;
    }
    
}