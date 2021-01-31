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
    [SerializeField] AudioSource audioSource;
    [SerializeField] ScrollableTextMaker tafel;
    [SerializeField] RawImage leinwand;
    [SerializeField] RenderTexture blanko;
    [SerializeField] GameObject menuoverlay;
    int foliennummer;
    int lernbereich;

    public void StartLerning (int lernbereich)
    {
        this.lernbereich = lernbereich;
        menuoverlay.SetActive(false);
        lernOverlay.SetActive(true);
        exerciseOverlay.SetActive(false);
        zurück.gameObject.SetActive(false);
        weiter.gameObject.SetActive(true);
        ton.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
        foliennummer = 0;
        tafel.Text = XMLImporter.learnData[lernbereich].learncontents[foliennummer].tafelanschrieb;
        SetImage();
    }
    public void NewNotes ()
    {
        zurück.gameObject.SetActive(true);
        foliennummer++;
        ton.gameObject.GetComponentInChildren<Text>().text = "Ton";

        if (foliennummer == XMLImporter.learnData[lernbereich].learncontents.Count)
        {
            gameObject.GetComponent<ExerciseEnvironment>().StartExercise(lernbereich,1, false);
        }
        else
        {
            tafel.Text = XMLImporter.learnData[lernbereich].learncontents[foliennummer].tafelanschrieb;
            SetImage();
        }
        
    }
    
    public void LastNotes()
    {
        foliennummer--;
        SetImage();
        tafel.Text = XMLImporter.learnData[lernbereich].learncontents[foliennummer].tafelanschrieb;
        if (foliennummer == 0)
        {
            zurück.gameObject.SetActive(false);
        }

    }
    public void AudioWiedergabe()
    {
        pause.gameObject.SetActive(true);
        ton.gameObject.GetComponentInChildren<Text>().text = "Erneut Wiedergeben";
        pause.gameObject.GetComponentInChildren<Text>().text = "Pause";
        audioSource.clip = XMLImporter.learnData[lernbereich].learncontents[foliennummer].audioClip;
        audioSource.Play();
        
    }

    public void ToggleAudio()
    {
        if (audioSource.isPlaying)
        {
            pause.gameObject.GetComponentInChildren<Text>().text = "Play";
            audioSource.Pause();
        }
        else
        {
            pause.gameObject.GetComponentInChildren<Text>().text = "Pause";
            audioSource.Play();
        }
        
    }

    void SetImage()
    {
        if (XMLImporter.learnData[lernbereich].learncontents[foliennummer].bild != null)
        {
            leinwand.texture = XMLImporter.learnData[lernbereich].learncontents[foliennummer].bild;
        }
        else
        {
            leinwand.texture = blanko;
        }
    }
}