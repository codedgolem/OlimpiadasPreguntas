using System;
using UnityEngine;

[Serializable]
public class Student : Person
{
    [SerializeField] private string courseS;
    [SerializeField] private string codeS;

    public Student()
    {
    }

    public Student(string courseS, string codeS, string nameP, string mailP, int ageP) : base(nameP, mailP, ageP)
    {
        this.courseS = courseS;
        this.codeS = codeS;
    }

    public string CourseS { get => courseS; set => courseS = value; }
    public string CodeS { get => codeS; set => codeS = value; }
}
