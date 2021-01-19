using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnEnvironment : MonoBehaviour
{
    //Variablen:
    [SerializeField] GameObject lernOverlay;
    [SerializeField] GameObject exerciseOverlay;
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
        tafel.text = XMLImporter.learnData[lernbereich].learncontents[foliennummer].tafelanschrieb;

    }
    public void NewNotes ()
    {
        zurück.gameObject.SetActive(true);
        foliennummer++;
        if (foliennummer == XMLImporter.learnData[lernbereich].learncontents.Count)
        {
            tafel.text = "Ende";
            foliennummer--;
        }
        else
        {
            tafel.text = XMLImporter.learnData[lernbereich].learncontents[foliennummer].tafelanschrieb;
        }
        
    }
    public void LastNotes()
    {
        foliennummer--;
        tafel.text = XMLImporter.learnData[lernbereich].learncontents[foliennummer].tafelanschrieb;
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
