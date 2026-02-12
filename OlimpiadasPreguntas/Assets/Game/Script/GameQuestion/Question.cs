using UnityEngine;

public abstract class Question
{
    private string pregunta;
   

    public Question(string pregunta)
    {
        this.pregunta = pregunta;
       
    }

    public string Pregunta { get => pregunta; set => pregunta = value; }

}
