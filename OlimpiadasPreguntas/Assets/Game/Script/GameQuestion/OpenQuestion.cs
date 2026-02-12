using UnityEngine;

public class OpenQuestion : Question
{
    private string answer;
    private string  versiculo;
    private string difficult;


    public OpenQuestion(string pregunta, string answer, string versiculo, string difficult) : base (pregunta)
    {
        this.answer = answer;
        this.versiculo = versiculo;
        this.difficult = difficult;
    }

    public string Answer { get => answer; set => answer = value; }
    public string Versiculo { get => versiculo; set => versiculo = value; }
    public string Difficult { get => difficult; set => difficult = value; }
}
