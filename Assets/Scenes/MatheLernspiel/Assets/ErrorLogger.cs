using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorLogger : MonoBehaviour
{
    [SerializeField] GameObject errorLogHolder;
    [SerializeField] Text errorText;
    public static List<string> importErrors = new List<string>();
    public static List<object> importingObjects = new List<object>();

    public void DisplayErrors()
    {
        if (importingObjects.Count != 0) return;
        if (importErrors.Count > 0)
        {
            string text = "Folgende Probleme sind beim Dateiimport aufgetreten: \n ";
            foreach(string error in importErrors)
            {
                text += " \n ";
                text += error + " \n ";
            }
            errorText.text = text;
        }
        else
        {
            errorText.text = "Alle Dateien wurden erfolgreich importiert";
        }
    }

    public void Close()
    {
        errorLogHolder.SetActive(false);
    }
}
