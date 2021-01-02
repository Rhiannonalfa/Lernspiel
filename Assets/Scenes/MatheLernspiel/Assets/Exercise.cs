public class Exercise
{
    public string aufgabe;
    public string lösung;
    public int schwierigkeitsgrad;

    public Exercise()
    {

    }

    public Exercise(string aufgabe, string lösung, int schwierigkeitsgrad)
    {
        this.aufgabe = aufgabe;
        this.lösung = lösung;
        this.schwierigkeitsgrad = schwierigkeitsgrad;
    }
}