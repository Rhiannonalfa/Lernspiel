public class Exercise
{
    public string aufgabe;
    public string lösung;
    public int schwierigkeitsgrad;
    public string lösungsweg;

    public Exercise ()
    {

    }


    public Exercise(string aufgabe, string lösung, string lösungsweg,  int schwierigkeitsgrad)
    {
        this.aufgabe = aufgabe;
        this.lösung = lösung;
        this.lösungsweg = lösungsweg;
        this.schwierigkeitsgrad = schwierigkeitsgrad;
    }
}