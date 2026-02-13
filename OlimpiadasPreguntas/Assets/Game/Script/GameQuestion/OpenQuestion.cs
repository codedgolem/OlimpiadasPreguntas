using UnityEngine;

public class AbiertasQuestion : Question
{
    private string answer;
    private string  versiculo;
    private string difficult;


    public AbiertasQuestion(string pregunta, string answer, string versiculo, string difficult) : base (pregunta)
    {
        this.answer = answer;
        this.versiculo = versiculo;
        this.difficult = difficult;
    }

    public string Answer { get => answer; set => answer = value; }
    public string Versiculo { get => versiculo; set => versiculo = value; }
    public string Difficult { get => difficult; set => difficult = value; }
}
