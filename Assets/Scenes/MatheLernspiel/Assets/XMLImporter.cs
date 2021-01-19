using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLImporter : MonoBehaviour
{
    public static List<LearnData> learnData;
    public static List<ExerciseData> exerciseData;

    private void Awake()
    {
        learnData = new List<LearnData>();
        exerciseData = new List<ExerciseData>();
        ImportFile("Assets\\Schnittpunktbestimmung.xml");
        ImportFile("Assets\\LSchnittpunktsbestimmung.xml");
    }

    public async void ImportFile(string fileName)
    {
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Async = true;
        XmlReader reader = XmlReader.Create(fileName, settings);
        string element = "";

        ExerciseData exerciseData = new ExerciseData(new List<Exercise>());
        Exercise exercise = new Exercise();
        LearnData learnData = new LearnData(new List<Learncontent>());
        Learncontent learncontent = new Learncontent();

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
                else if (reader.Name == "Folie")
                {
                    learncontent = new Learncontent();
                }

            }
            else if (reader.NodeType == XmlNodeType.Text)
            {
                switch (element)
                {
                //XML-Aufgaben
                    case "Thema":
                        //reader.Value;
                        break;
                    case "Difficulty":
                        exercise.schwierigkeitsgrad = int.Parse(reader.Value);
                        break;
                    case "Question":
                        exercise.aufgabe = reader.Value;
                        break;
                    case "Picture":
                        //to do: BildPlatzhalter eingügen in UI + Methode schreiben zum darstellen
                        break;
                    case "Solution":
                        exercise.lösung = reader.Value;
                        break;
                    case "Way":
                        exercise.lösungsweg = reader.Value;
                        break;

                //XML-Lerninhalte
                    case "Bild":
                        // todo: Bild Platzhalter...
                        break;
                    case "Ton":
                        //Ton einbinden etc.
                        break;
                    case "Text":
                        learncontent.tafelanschrieb = reader.Value;
                        break;
                }
            }
            if (reader.NodeType == XmlNodeType.EndElement)
            {
                if (reader.Name == "Exercise")
                {
                    exerciseData.exercises.Add(exercise);
                }
                else if (reader.Name == "Exercises")
                {
                    XMLImporter.exerciseData.Add(exerciseData);
                    exerciseData = new ExerciseData(new List<Exercise>());
                }
                else if (reader.Name == "Folie")
                {
                    learnData.learncontents.Add(learncontent);
                }
                else if (reader.Name == "Lerninhalt")
                {
                    XMLImporter.learnData.Add(learnData);
                    learnData = new LearnData(new List<Learncontent>());
                }
            }


        }

    }
}