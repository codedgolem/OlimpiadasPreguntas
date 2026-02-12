using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.IO;
using System;

[Serializable]
public class DatosGuardados{
    public List<Student> list_studentsD = new List<Student>();
}

public class ControllerScene_3 : MonoBehaviour
{
    
    List<Student> list_students = new List<Student>();

    public TMP_InputField tnameS;
    public TMP_InputField tmailS;
    public TMP_InputField tageS;
    public TMP_InputField tcourseS;
    public TMP_InputField tcodeS;
    public TextMeshProUGUI timpresionS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddStudent()
    {
        Student student = new Student(tcourseS.text, tcodeS.text, tnameS.text, tmailS.text, int.Parse(tageS.text));
        list_students.Add(student);
    }

    public void PrintStudents()
    {
        string info = "";
        foreach (Student s in list_students)
        {
            info += "Student: " + s.NameP + ", Course: " + s.CourseS + " Code: " + s.CodeS + " Mail: " + s.MailP + " Age: " + s.AgeP + " ///// ";
        }
        
        Debug.Log(info);
    }

    public void PrintTMP()
    {
        string info = "";
        foreach (Student s in list_students)
        {
            info += "Student: " + s.NameP + ", Course: " + s.CourseS + " Code: " + s.CodeS + " Mail: " + s.MailP + " Age: " + s.AgeP + " \n ------ \n";
        }
        
        timpresionS.text = info;
    }

    //Recuerda que para guardar en JSON, el objeto a guardar debe ser serializable, es decir, debe tener el atributo [Serializable] y
    //sus campos deben ser públicos o tener propiedades públicas como el list. Ademas el persistentdatapath es para guardar datos que persistan entre
    //sesiones de juego
    public void SaveJSON()
    {
        string path = Application.persistentDataPath + "/students.json";
        DatosGuardados datos = new DatosGuardados();
        datos.list_studentsD = list_students;
        string infoJson = JsonUtility.ToJson(datos);
        Debug.Log(infoJson);

        

        File.WriteAllText(path, infoJson);

        Debug.Log("Datos guardados en: " + path);
    }

    public void LoadJson()
    {
        string path = Application.dataPath + "/students.json";
        if (File.Exists(path)) {
            string infoJson = File.ReadAllText(path);
            DatosGuardados datos = JsonUtility.FromJson<DatosGuardados>(infoJson);
            list_students.AddRange(datos.list_studentsD);
            Debug.Log("Datos cargados desde: " + path + " Cantidad de objetos: " + list_students.Count);
        } else {
            Debug.Log("No existe el archivo en: " + path);
        }
    }
}
