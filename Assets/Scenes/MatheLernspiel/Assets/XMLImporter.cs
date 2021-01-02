using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLImporter : MonoBehaviour
{
    public static LearnData[] learnData;
    public static List<ExerciseData> exerciseData;



    private void Awake()
    {
        learnData = new LearnData[] { new LearnData(new string[] { "aaaaaaaaaaaa", "bbbbbbbbbbbbbbbb", "cccccccccccc" }) };
        exerciseData = new List<ExerciseData> { new ExerciseData(new Exercise[] { new Exercise("AAAAAAAA", "1", 0), new Exercise("BBBBBBBBB", "2", 0), new Exercise("CCCCCCCC", "3", 0) }) };
        ImportFile("C:\\Users\\Lill Kuhfahl\\Documents\\GameDevelopment\\MatheLernspiel\\Assets\\Scenes\\MatheLernspiel\\Assets\\Schnittpunktbestimmung.xml");
    }

    public async void ImportFile(string fileName)
    {
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Async = true;
        XmlReader reader = XmlReader.Create(fileName, settings);
        string element = "";

        Exercise exercise = new Exercise();
        while (await reader.ReadAsync())
        {

            if (reader.NodeType == XmlNodeType.Element)
            {
                element = reader.Name;
                Debug.Log(reader.Name);
                if (reader.Name == "Exercise")
                {
                    exercise = new Exercise();
                }

            }
            else if (reader.NodeType == XmlNodeType.Text)
            {
                switch (element)
                {
                    case "Thema":
                        //reader.Value;
                        break;
                    case "Difficulty":
                        exercise.schwierigkeitsgrad = int.Parse(reader.Value);
                        break;

                }
            }
            if (reader.NodeType == XmlNodeType.EndElement)
            {
                if (reader.Name == "Exercises")
                {
                    ExerciseData exerciseData = new ExerciseData ();
                    Debug.Log("Juhuuuu!");
                }

            }


        }

    }
}