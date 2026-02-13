using JetBrains.Annotations;
using NUnit.Framework;
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
    //Para paneles
    public GameObject panelOpen;
    public GameObject panelTrueFalse;
    public GameObject panelMultiple;
    //para truefalse questions
    //
    //Para open questions
    public TextMeshProUGUI answerOpen;
    public TextMeshProUGUI versiculoOpen;

    //Lista de preguntas o variables inciales
    List<Question> list_questions = new List<Question>();
    Question currentQuestion;
    int index;
    int lastIndex = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        list_questions.Clear();
        LoadQuestionsMultiple("SELECCION_MULTIPLE_2024.txt");
        LoadQuestionsMultiple("ArchivoPreguntas.txt");
        LoadQuestionsTrueFalse("FALSO_VERDADERO_2024.txt");
        LoadQuestionsOpen("ABIERTAS_2024.txt");
        ShowInScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInScene()
    {
        do { 
        index = UnityEngine.Random.Range(0, list_questions.Count);
        }
        while (index == lastIndex) ;

        lastIndex = index;
        currentQuestion = list_questions[index];

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
                ShowOpenQuestion();
            }
        } else
        {
            Debug.Log("No se pudo cargar la pregunta");
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
            list_questions.Add(question);
            


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
            list_questions.Add(question);
           
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
            list_questions.Add(question);
            
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
                if (option2.text == multipleQ.Answer)
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
                if (option3.text == multipleQ.Answer)
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
                if (option4.text == multipleQ.Answer)
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
        Debug.Log("Pregunta mostrada: " + tf.Question);
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

    public void ShowOpenQuestion()
    {
        panelMultiple.SetActive(false);
        panelTrueFalse.SetActive(false);
        panelOpen.SetActive(true);
        question.text = currentQuestion.Pregunta;
        answerOpen.text = "";
        versiculoOpen.text = "";
    }
     public void OpenAnswerVerify()
    {
        AbiertasQuestion openQ = (AbiertasQuestion)currentQuestion;
        answerOpen.text = openQ.Answer;
        versiculoOpen.text = openQ.Versiculo;
    }
}
