using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Learncontent 
{
    // to do: bild und ton einfügen
    public string tafelanschrieb;
    public Texture2D bild;
    public AudioClip audioClip;

    public Learncontent()
    {
        
    }

    public Learncontent(string tafelanschrieb, Texture2D bild)
    {
        this.tafelanschrieb = tafelanschrieb;
        this.bild = bild;
    }

    public IEnumerator GetAudioClip(string url, ErrorLogger errorLogger)
    {
        
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + Application.streamingAssetsPath + url, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                ErrorLogger.importErrors.Add("Audiodatei konnte nicht gefunden werden: " + url);
            }
            else
            {
                try
                {
                    audioClip = DownloadHandlerAudioClip.GetContent(www);
                }
                catch 
                {
                    ErrorLogger.importErrors.Add("Audiodatei konnte nicht gefunden werden: " + url);
                }               
            }
        }
        ErrorLogger.importingObjects.Remove(this);
        errorLogger.DisplayErrors();
    }
}
