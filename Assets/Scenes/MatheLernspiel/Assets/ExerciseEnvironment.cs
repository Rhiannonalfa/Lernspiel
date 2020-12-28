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
    [SerializeField] Button richtig;
    [SerializeField] Button falsch;
    [SerializeField] Text tafel;
    [SerializeField] InputField loesungen;
    float schwierigkeitsgrad;
    int aufgabennummer;
    int aufgabenindex;
    int maxaufgaben;
    int lernbereich = 0;
    bool check = false;

    public void StartExercise()
    {
        lernOverlay.SetActive(false);
        exerciseOverlay.SetActive(true);
        zurück.gameObject.SetActive(false);
        abgebenLösung.gameObject.SetActive(true);
        loesungen.gameObject.SetActive(true);
        nächsteAufgabe.gameObject.SetActive(false);
        richtig.gameObject.SetActive(false);
        falsch.gameObject.SetActive(false);
        aufgabennummer = 1;
        schwierigkeitsgrad = 0;
        aufgabenindex = Random.Range(0, XMLImporter.exerciseData[lernbereich].easyCount) ;
        tafel.text = XMLImporter.exerciseData[lernbereich].aufgaben[aufgabenindex];
    }
    public void CheckAnswer()
    {
        zurück.gameObject.SetActive(true);
        abgebenLösung.gameObject.SetActive(false);
        loesungen.gameObject.SetActive(true);
        nächsteAufgabe.gameObject.SetActive(true);
        loesungen.interactable = false;


        if (loesungen.text == XMLImporter.exerciseData[lernbereich].lösungen[aufgabenindex])
        {
            tafel.text = "Richtig!";
        }
        else
        {
            check = false;
            richtig.gameObject.SetActive(true);
            falsch.gameObject.SetActive(true);
            tafel.text = "Die richtige Lösung ist: " + XMLImporter.exerciseData[lernbereich].lösungen[aufgabenindex] + " Prüfe deine Antwort!";
        }
    }
   public void CheckAnswerStudendRight ()
    {
        if (check) return;
        check = true;
        schwierigkeitsgrad = schwierigkeitsgrad + 0.34f ;
        Debug.Log(schwierigkeitsgrad);
    }
    public void CheckAnswerStudendWrong()
    {
        if (check) return;
        check = true;
        schwierigkeitsgrad = schwierigkeitsgrad - 0.34f;
        if (schwierigkeitsgrad < 0)
        {
            schwierigkeitsgrad = 0;
        } 
    }

    public void NewExercise ()
    {
        abgebenLösung.gameObject.SetActive(true);
        loesungen.gameObject.SetActive(true);
        nächsteAufgabe.gameObject.SetActive(false);
        richtig.gameObject.SetActive(false);
        falsch.gameObject.SetActive(false);
        zurück.gameObject.SetActive(false);
        loesungen.interactable = true;
        aufgabennummer++;
        if (aufgabennummer == maxaufgaben+1 )
        {
            tafel.text = "Du hast die Übung abgeschlossen!";
            aufgabennummer--;
        }
        else
        {
            if (schwierigkeitsgrad <1)
            {
                aufgabenindex = Random.Range(0, XMLImporter.exerciseData[lernbereich].easyCount);
                tafel.text = XMLImporter.exerciseData[lernbereich].aufgaben[aufgabenindex];
            }
           else if (schwierigkeitsgrad < 2)
            {
                aufgabenindex = Random.Range(XMLImporter.exerciseData[lernbereich].easyCount, XMLImporter.exerciseData[lernbereich].easyCount + XMLImporter.exerciseData[lernbereich].medCount);
                tafel.text = XMLImporter.exerciseData[lernbereich].aufgaben[aufgabenindex];
            }
            else
            {
                aufgabenindex = Random.Range(XMLImporter.exerciseData[lernbereich].easyCount + XMLImporter.exerciseData[lernbereich].medCount, XMLImporter.exerciseData[lernbereich].easyCount+ XMLImporter.exerciseData[lernbereich].medCount+ XMLImporter.exerciseData[lernbereich].hardCount);
                tafel.text = XMLImporter.exerciseData[lernbereich].aufgaben[aufgabenindex];
            }
        }

    }
    public void LastExercise()
    {
        loesungen.interactable = false;
        abgebenLösung.gameObject.SetActive(true);
        loesungen.gameObject.SetActive(true);
        nächsteAufgabe.gameObject.SetActive(false);
        richtig.gameObject.SetActive(false);
        falsch.gameObject.SetActive(false);
        zurück.gameObject.SetActive(false);
        tafel.text = XMLImporter.exerciseData[lernbereich].aufgaben[aufgabenindex];
    }
}

   
  
