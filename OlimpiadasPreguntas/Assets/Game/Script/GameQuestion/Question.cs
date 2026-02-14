using System;
using UnityEngine;

public abstract class Question
{
    private string pregunta;
    private bool estado;

    protected Question(string pregunta, bool estado)
    {
        this.pregunta = pregunta;
        this.estado = estado;
    }

    public string Pregunta { get => pregunta; set => pregunta = value; }
    public bool Estado { get => estado; set => estado = value; }
}
