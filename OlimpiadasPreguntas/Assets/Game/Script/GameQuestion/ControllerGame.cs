using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
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
    //Para paneles
    public GameObject panelOpen;
    public GameObject panelTrueFalse;
    public GameObject panelMultiple;
    public GameObject panelNextLevel;
    //para truefalse questions
    //
    //Para open questions
    public TextMeshProUGUI answerOpen;
    public TextMeshProUGUI versiculoOpen;

    //Lista de preguntas o variables inciales
    List<Question> list_questionsEasy = new List<Question>();
    List<Question> list_questionsHard = new List<Question>();
    Question currentQuestion;
    int index;
    int lastIndexE = -1;
    int lastIndexH = -1;
    int numEasy;
    int numHard;
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
        ShowInScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInScene()
    {
        Debug.Log(list_questionsEasy.Count);
        Debug.Log(numEasy);
        if (list_questionsEasy.Count != numEasy)
        { 
            do { 
            index = UnityEngine.Random.Range(0, list_questionsEasy.Count); // genera un numero de pregunta al azar entre todas la lista de preguntas 
            }
            while (index == lastIndexE || list_questionsEasy[index].Estado == true) ;
            numEasy += 1;

            lastIndexE = index; // si es igual al de la pregunta anterior le dice que vuelva a buscar otra pregunta al azar porque esa ya salio
            //solo cuando el numero o el indice de la pregunta es diferente , este ciclo se rompe y el programa continua
            currentQuestion = list_questionsEasy[index]; // acceso por indice // tomara el indice de la pregunta que le pedimos arriba que buscara aleatoriamente y lo convertira en la pregunta actual  
            //bloque para evitar que salga la misma pregunta dos veces seguidas 

            if (currentQuestion != null) // validacion de seguridad
                // null= nada o vacio , si la pregunta existe y no esta vacio entonces podra mostrarla en pantalla 
            {
                if (currentQuestion is MultipleQuestion) // la palabra clave is se utiliza para que el programa entienda que tipo de pregunta tiene que prosesar o mostrar
                // la palabra clave is actua como un clasificador para mandar cada pregunta al panel correcto 
                {
                    MultipleQuestion multipleQ = (MultipleQuestion)currentQuestion;
                   //si la pregunta actual es de tipo multiple has esto;
                    ShowMultipleQuestion(multipleQ);
                }
                else if (currentQuestion is TrueFalseQuestion)
                {
                    TrueFalseQuestion trueFalseQ = (TrueFalseQuestion)currentQuestion;
                    ShowTrueFalseQuestion(trueFalseQ);
                }
                else if (currentQuestion is AbiertasQuestion)
                {
                    AbiertasQuestion openQ = (AbiertasQuestion)currentQuestion;
                    ShowOpenQuestion(openQ);
                }
            } 
        }else if (list_questionsHard.Count != numHard)
        {
            Debug.Log("ingreso");
            if (numHard == 0)
            {
                panelNextLevel.SetActive(true);
                title.text = "¡Felicidades! Has completado el nivel facil.";
                description.text = "Prepárate para el siguiente nivel con preguntas aún más desafiantes.";
            }
            {
                title.text = "¡Felicidades! Has completado el nivel facil.";
                description.text = "Prepárate para el siguiente nivel con preguntas aún más desafiantes.";
            }
            do
            {
                index = UnityEngine.Random.Range(0, list_questionsHard.Count);
                Debug.Log("int");
            }
            while (index == lastIndexH || list_questionsHard[index].Estado == true); 

            lastIndexH = index; 
            numHard += 1;   
            currentQuestion = list_questionsHard[index]; 

            if (currentQuestion != null) 
                                        
            {
                if (currentQuestion is MultipleQuestion) 
                {
                    MultipleQuestion multipleQ = (MultipleQuestion)currentQuestion;
                    ShowMultipleQuestion(multipleQ);
                }
                else if (currentQuestion is TrueFalseQuestion)
                {
                    TrueFalseQuestion trueFalseQ = (TrueFalseQuestion)currentQuestion;
                    ShowTrueFalseQuestion(trueFalseQ);
                }
                else if (currentQuestion is AbiertasQuestion)
                {
                    AbiertasQuestion openQ = (AbiertasQuestion)currentQuestion;
                    ShowOpenQuestion(openQ);
                }
            }
        }else
        {
          panelNextLevel.SetActive(true);
          title.text = "Ya no hay mas preguntas";
            description.text = "Gracias por participar";
        }
    }
    //LOAD QUESTIONS --------------------------------------------
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

    //MULTIPLE QUESTIONS --------------------------------------------

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

    public void MultipleAnswerVerify(int option)
    {
        MultipleQuestion multipleQ = (MultipleQuestion)currentQuestion;
        switch (option)
        {
            case 1:
                if (option1.text.Equals(multipleQ.Answer))
                {
                    title.text = "Correcto";
                    description.text = multipleQ.Versiculo;
                }
                else
                {
                    title.text = "Incorrecto";
                    description.text = "Intenta de nuevo";
                }
                break;
            case 2:
                if (option2.text.Equals(multipleQ.Answer))
                {
                    title.text = "Correcto";
                    description.text = multipleQ.Versiculo;
                }
                else
                {
                    title.text = "Incorrecto";
                    description.text = "Intenta de nuevo";
                }
                break;
            case 3:
                if (option3.text.Equals(multipleQ.Answer))
                {
                    title.text = "Correcto";
                    description.text = multipleQ.Versiculo;
                }
                else
                {
                    title.text = "Incorrecto";
                    description.text = "Intenta de nuevo";
                }
                break;
            case 4:
                if (option4.text.Equals(multipleQ.Answer))
                {
                    title.text = "Correcto";
                    description.text = multipleQ.Versiculo;
                }
                else
                {
                    title.text = "Incorrecto";
                    description.text = "Intenta de nuevo";
                }
                break;
        }
    }

    //TRUE FALSE QUESTIONS --------------------------------------------

    public void ShowTrueFalseQuestion(TrueFalseQuestion tf)
    {
        panelMultiple.SetActive(false);
        panelOpen.SetActive(false);
        panelTrueFalse.SetActive(true);
        question.text = tf.Pregunta;
        difficulty.text = tf.Difficult;
        

        tf.Estado = true;
    }

        public void TrueFalseAnswerVerify(bool option)
        {
            TrueFalseQuestion trueFalseQ = (TrueFalseQuestion)currentQuestion;
            if (option == trueFalseQ.Answer)
            {
                title.text = "Correcto";
                description.text = trueFalseQ.Versiculo;
            }
            else
            {
                title.text = "Incorrecto";
                description.text = "Intenta de nuevo";
            }
    }

    //OPEN QUESTIONS --------------------------------------------

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
     public void OpenAnswerVerify()
    {
        AbiertasQuestion openQ = (AbiertasQuestion)currentQuestion;
        answerOpen.text = openQ.Answer;
        versiculoOpen.text = openQ.Versiculo;
    }
}
