using UnityEngine;

public class TrueFalseQuestion : Question
{ 
    private string question;
    private bool answer;
    private string versiculo;
    private string difficult;
    public TrueFalseQuestion(string question, bool answer, string versiculo, string difficult) : base (question, false)
    {
        this.answer = answer;
        this.versiculo = versiculo;
        this.difficult = difficult;
    }
    public string Question { get => question; set => question = value; }
    public bool Answer { get => answer; set => answer = value; }
    public string Versiculo { get => versiculo; set => versiculo = value; }
    public string Difficult { get => difficult; set => difficult = value; }
}
