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
    [SerializeField] ScrollableTextMaker tafel;
    [SerializeField] InputField loesungen;
    [SerializeField] RawImage leinwand;
    [SerializeField] RenderTexture blanko;
    [SerializeField] GameObject menuoverlay;
    float schwierigkeitsgrad;
    int aufgabennummer;
    int aufgabenindex;
    int maxaufgaben;
    int lernbereich;
    bool check = false;


    public void StartExercise(int lernbereich, int maxAufgaben, bool randomize = true)
    {
        this.lernbereich = lernbereich;
        menuoverlay.SetActive(false);
        lernOverlay.SetActive(false);
        exerciseOverlay.SetActive(true);
        zurück.gameObject.SetActive(false);
        abgebenLösung.gameObject.SetActive(true);
        loesungen.gameObject.SetActive(true);
        nächsteAufgabe.gameObject.SetActive(false);
        richtig.gameObject.SetActive(false);
        falsch.gameObject.SetActive(false);
        loesungen.text = "";

        aufgabennummer = 1;
        schwierigkeitsgrad = 0;
        this.maxaufgaben = maxAufgaben;
        if (!randomize)
        {
            aufgabenindex = 0;
        }
        else
        {
            aufgabenindex = Random.Range(0, XMLImporter.learnData[lernbereich].exerciseData.easyCount);
        }
        tafel.Text = XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].aufgabe;
        SetImageQuestion();
    }
    public void CheckAnswer()
    {
        zurück.gameObject.SetActive(true);
        abgebenLösung.gameObject.SetActive(false);
        loesungen.gameObject.SetActive(true);
        nächsteAufgabe.gameObject.SetActive(true);
        loesungen.interactable = false;
        if (maxaufgaben == aufgabennummer)
        {
            nächsteAufgabe.gameObject.GetComponentInChildren<Text>().text = "Beenden";
        }

        if (loesungen.text == XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].lösung)
        {
            tafel.Text = "Richtig!";
            SetImageAnswer();
        }
        else
        {
            check = false;
            nächsteAufgabe.gameObject.SetActive(false);
            richtig.gameObject.SetActive(true);
            falsch.gameObject.SetActive(true);
            tafel.Text = "Die richtige Lösung ist: " + XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].lösungsweg + " Prüfe deine Antwort!";
            SetImageAnswer();
        }
    }
   public void CheckAnswerStudendRight ()
    {
        if (check) return;
        check = true;
        nächsteAufgabe.gameObject.SetActive(true);
        schwierigkeitsgrad = schwierigkeitsgrad + 0.34f ;
        Debug.Log(schwierigkeitsgrad);
    }
    public void CheckAnswerStudendWrong()
    {
        if (check) return;
        check = true;
        nächsteAufgabe.gameObject.SetActive(true);
        schwierigkeitsgrad = schwierigkeitsgrad - 1f;
        if (schwierigkeitsgrad < 0)
        {
            schwierigkeitsgrad = 0;
        } 
        else if (schwierigkeitsgrad - Mathf.FloorToInt(schwierigkeitsgrad) >= 0.34f)
        {
            schwierigkeitsgrad = Mathf.CeilToInt(schwierigkeitsgrad);
        }
    }

    public void NewExercise ()
    {
        if (check == false) return;
        abgebenLösung.gameObject.SetActive(true);
        loesungen.gameObject.SetActive(true);
        nächsteAufgabe.gameObject.SetActive(false);
        richtig.gameObject.SetActive(false);
        falsch.gameObject.SetActive(false);
        zurück.gameObject.SetActive(false);
        loesungen.interactable = true;
        loesungen.text = "";
        aufgabennummer++;

        if (aufgabennummer == maxaufgaben+1 )
        {
            exerciseOverlay.gameObject.SetActive(false);
            menuoverlay.gameObject.SetActive(true);
            nächsteAufgabe.gameObject.GetComponentInChildren<Text>().text = "Nächste Aufgabe";

        }
        else
        {
            if (schwierigkeitsgrad <1)
            {
                aufgabenindex = Random.Range(0, XMLImporter.learnData[lernbereich].exerciseData.easyCount);
                tafel.Text = XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].aufgabe;
                SetImageQuestion();
            }
           else if (schwierigkeitsgrad < 2)
            {
                aufgabenindex = Random.Range(XMLImporter.learnData[lernbereich].exerciseData.easyCount, XMLImporter.learnData[lernbereich].exerciseData.easyCount + XMLImporter.learnData[lernbereich].exerciseData.medCount);
                tafel.Text = XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].aufgabe;
                SetImageQuestion();
            }
            else
            {
                aufgabenindex = Random.Range(XMLImporter.learnData[lernbereich].exerciseData.easyCount + XMLImporter.learnData[lernbereich].exerciseData.medCount, XMLImporter.learnData[lernbereich].exerciseData.easyCount+ XMLImporter.learnData[lernbereich].exerciseData.medCount+ XMLImporter.learnData[lernbereich].exerciseData.hardCount);
                tafel.Text = XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].aufgabe;
                SetImageQuestion();
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
        tafel.Text = XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].aufgabe;
        SetImageQuestion();
    }

    void SetImageQuestion()
    {
        if (XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].picturequestion != null)
        {
            leinwand.texture = XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].picturequestion;
        }
        else
        {
            leinwand.texture = blanko;
        }
    }

    void SetImageAnswer()
    {
        if (XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].pictureanswer != null)
        {
            leinwand.texture = XMLImporter.learnData[lernbereich].exerciseData.exercises[aufgabenindex].pictureanswer;
        }
        else
        {
            leinwand.texture = blanko;
        }
    }
}

   
