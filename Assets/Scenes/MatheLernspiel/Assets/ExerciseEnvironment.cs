using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseEnvironment : MonoBehaviour
{
    [SerializeField] Button weiter;
    [SerializeField] Button zurück;
    [SerializeField] Button ton;
    [SerializeField] Button pause;
    [SerializeField] Text tafel;
    [SerializeField] InputField loesungen;
    int schwierigkeitsgrad;
    int aufgabennummer;
    int maxaufgaben;
    int lernbereich = 0;

    public void StartExercise()
    {
        zurück.gameObject.SetActive(false);
        weiter.gameObject.SetActive(true);
        ton.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
        loesungen.gameObject.SetActive(true);

        tafel.text = "Wie viele Aufgaben möchtest du bearbeiten? ";

    }

    public void SetMaxAufgaben ()
    {

    }
  
}
