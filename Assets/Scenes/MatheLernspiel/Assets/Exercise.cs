using UnityEngine;

public class Exercise
{
    public string aufgabe;
    public string lösung;
    public int schwierigkeitsgrad;
    public string lösungsweg;
    public Texture2D picturequestion;
    public Texture2D pictureanswer;

    public Exercise ()
    {

    }


    public Exercise(string aufgabe, string lösung, string lösungsweg,  int schwierigkeitsgrad, Texture2D picturequestion, Texture2D pictureanswer)
    {
        this.aufgabe = aufgabe;
        this.lösung = lösung;
        this.lösungsweg = lösungsweg;
        this.schwierigkeitsgrad = schwierigkeitsgrad;
        this.picturequestion = picturequestion;
        this.pictureanswer = pictureanswer;
    }
}