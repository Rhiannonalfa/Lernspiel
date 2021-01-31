using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

public class XMLImporter : MonoBehaviour
{
    public static List<LearnData> learnData;
    static List<ExerciseData> exerciseData;
    ErrorLogger errorLogger;
    int fileCount = 0;
    int filesDone = 0;

    private void Awake()
    {
        ErrorLogger.importingObjects.Add(this);
        errorLogger = gameObject.GetComponent<ErrorLogger>();
        learnData = new List<LearnData>();
        exerciseData = new List<ExerciseData>();
        ImportFile(Application.streamingAssetsPath + "\\XML\\LEinführungFunktionen.xml");
        ImportFile(Application.streamingAssetsPath + "\\XML\\LProportionaleFunktionen.xml");
        ImportFile(Application.streamingAssetsPath + "\\XML\\LGraphischeDarstellungvonproportionalenFunktionen.xml");
        ImportFile(Application.streamingAssetsPath + "\\XML\\LLineareFunktionen.xml");
        /*ImportFile(Application.streamingAssetsPath + "\\XML\\LSchnittpunktsbestimmung.xml");*/
        ImportFile(Application.streamingAssetsPath + "\\XML\\EinführungFunktionen.xml");
        ImportFile(Application.streamingAssetsPath + "\\XML\\ProportionaleFunktionen.xml");
        ImportFile(Application.streamingAssetsPath + "\\XML\\GraphischeDarstellungvonproportionalenFunktionen.xml");
        ImportFile(Application.streamingAssetsPath + "\\XML\\LineareFunktionen.xml");
        /*ImportFile(Application.streamingAssetsPath + "\\XML\\Schnittpunktbestimmung.xml");*/
    }

    public async void ImportFile(string fileName)
    {
        fileCount++;
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Async = true;
        XmlReader reader = XmlReader.Create(fileName, settings);
        string element = "";

        ExerciseData exerciseData = new ExerciseData(new List<Exercise>(),0);
        Exercise exercise = new Exercise();
        List<Exercise> exercises = new List<Exercise>();
        LearnData learnData = new LearnData(new List<Learncontent>(), "");
        Learncontent learncontent = new Learncontent();

        while (await reader.ReadAsync())
        {

            if (reader.NodeType == XmlNodeType.Element)
            {
                element = reader.Name;
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
                        learnData.theme = reader.Value;
                        break;
                    case "Nummer":
                        learnData.number = int.Parse(reader.Value);
                        break;
                    case "ENummer":
                        exerciseData.number = int.Parse(reader.Value);
                        break;
                    case "Difficulty":
                        exercise.schwierigkeitsgrad = int.Parse(reader.Value);
                        break;
                    case "Question":
                        exercise.aufgabe = reader.Value;
                        break;
                    case "Picture":
                        byte[] bytes = File.ReadAllBytes((Application.streamingAssetsPath + reader.Value));
                        exercise.picturequestion = new Texture2D(10, 10);
                        exercise.picturequestion.LoadImage(bytes);
                        break;
                    case "Sketch":
                        bytes = File.ReadAllBytes((Application.streamingAssetsPath + reader.Value));
                        exercise.pictureanswer = new Texture2D(10, 10);
                        exercise.pictureanswer.LoadImage(bytes);
                        break;
                    case "Solution":
                        exercise.lösung = reader.Value;
                        break;
                    case "Way":
                        exercise.lösungsweg = reader.Value;
                        break;

                //XML-Lerninhalte
                    case "Bild":
                        bytes = File.ReadAllBytes((Application.streamingAssetsPath + reader.Value));
                        learncontent.bild = new Texture2D(10,10);
                        learncontent.bild.LoadImage(bytes);
                        break;
                    case "Ton":
                        if (reader.Value == "") break;
                        ErrorLogger.importingObjects.Add(learncontent);
                        StartCoroutine(learncontent.GetAudioClip(reader.Value, errorLogger));
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
                    exercises.Add(exercise);
                }
                else if (reader.Name == "Exercises")
                {
                    exerciseData.SetList(exercises);
                    XMLImporter.exerciseData.Add(exerciseData);
                    TestDone();
                }
                else if (reader.Name == "Folie")
                {
                    learnData.learncontents.Add(learncontent);
                }
                else if (reader.Name == "Lerninhalt")
                {
                    XMLImporter.learnData.Add(learnData);
                    TestDone();
                }
            }
        }

    }


    void TestDone()
    {
        filesDone++;
        if (filesDone == fileCount)
        {
            foreach(ExerciseData singleExerciseData in exerciseData)
            {
                bool foundLearn = false;
                foreach(LearnData singleLearnData in learnData)
                {
                    if (singleExerciseData.number == singleLearnData.number)
                    {
                        singleLearnData.exerciseData = singleExerciseData;
                        singleLearnData.exerciseSet = true;
                        foundLearn = true;
                        break;
                    }
                }
                if (!foundLearn)
                {
                    ErrorLogger.importErrors.Add("Eine Übungsreihe hat keinen passenden Lerninhalt: " + singleExerciseData.number);
                }
            }
            learnData.Sort();

            foreach (LearnData singleLearnData in learnData)
            {
                if (!singleLearnData.exerciseSet)
                {
                    ErrorLogger.importErrors.Add("Ein Lernthema hat keinen passende Übungsreihe: " + singleLearnData.theme);
                }
            }
            ErrorLogger.importingObjects.Remove(this);
            errorLogger.DisplayErrors();
            gameObject.GetComponent<Menu>().GenerateMenu();
        }
    }
}