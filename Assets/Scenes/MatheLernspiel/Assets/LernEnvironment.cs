using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LernEnvironment : MonoBehaviour
{
    //Variablen:
    [SerializeField] GameObject lernOverlay;
    [SerializeField] GameObject exerciseOverlay;
    [SerializeField] Button nächsteAufgabe;
    [SerializeField] Button weiter;
    [SerializeField] Button zurück;
    [SerializeField] Button ton;
    [SerializeField] Button pause;
    [SerializeField] Text tafel;
    int foliennummer;
    int lernbereich = 0;

    public void StartLerning ()
    {
        lernOverlay.SetActive(true);
        exerciseOverlay.SetActive(false);
        zurück.gameObject.SetActive(false);
        weiter.gameObject.SetActive(true);
        ton.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
        foliennummer = 0;
        tafel.text = XMLImporter.learnData[lernbereich].Lerninhalte[foliennummer];

    }
    public void NewNotes ()
    {
        zurück.gameObject.SetActive(true);
        foliennummer++;
        if (foliennummer == XMLImporter.learnData[lernbereich].Lerninhalte.Length)
        {
            tafel.text = "Ende";
            foliennummer--;
        }
        else
        {
            tafel.text = XMLImporter.learnData[lernbereich].Lerninhalte[foliennummer];
        }
        
    }
    public void LastNotes()
    {
        foliennummer--;
        tafel.text = XMLImporter.learnData[lernbereich].Lerninhalte[foliennummer];
        if (foliennummer == 0)
        {
            zurück.gameObject.SetActive(false);
        }

    }
    public void AudioWiedergabe()
    {
        pause.gameObject.SetActive(true);
    }

}
