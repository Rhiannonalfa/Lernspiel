using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseEnvironment : MonoBehaviour
{
    [SerializeField] GameObject lernOverlay;
    [SerializeField] GameObject exerciseOverlay;
    [SerializeField] Button nächsteAufgabe;
    [SerializeField] Button abgebenLösung;
    [SerializeField] Button zurück;
    [SerializeField] Text tafel;
    [SerializeField] InputField loesungen;
    int schwierigkeitsgrad;
    int aufgabennummer;
    int aufgabenindex;
    int maxaufgaben;
    int lernbereich = 0;

    public void StartExercise()
    {
        lernOverlay.SetActive(false);
        exerciseOverlay.SetActive(true);
        zurück.gameObject.SetActive(false);
        abgebenLösung.gameObject.SetActive(true);
        loesungen.gameObject.SetActive(true);
        nächsteAufgabe.gameObject.SetActive(false);
        aufgabennummer = 1;
        schwierigkeitsgrad = 0;
        aufgabenindex = XMLImporter.exerciseData[lernbereich].easyCount;
        tafel.text = XMLImporter.exerciseData[lernbereich].aufgaben[Random.Range(0, aufgabenindex)];
    }
    public void CheckAnswer()
    {
        zurück.gameObject.SetActive(true);
        abgebenLösung.gameObject.SetActive(false);
        loesungen.gameObject.SetActive(true);
        nächsteAufgabe.gameObject.SetActive(true);

        if (loesungen.text == XMLImporter.exerciseData[lernbereich].lösungen[aufgabenindex])
        {
            tafel.text = "Richtig!";
        }
        else
        {
            tafel.text = "Die richtige Lösung ist: " + XMLImporter.exerciseData[lernbereich].lösungen[aufgabenindex] + " Prüfe deine Antwort!";
        }
    }

    public void NewExercise ()
    {
        zurück.gameObject.SetActive(true);
        aufgabennummer++;
        if (aufgabennummer == maxaufgaben+1 )
        {
            tafel.text = "Du hast die Übung abgeschlossen!";
            aufgabennummer--;
        }
        else
        {
            tafel.text = XMLImporter.exerciseData[lernbereich].aufgaben[schwierigkeitsgrad];
        }

    }
}

   
  
