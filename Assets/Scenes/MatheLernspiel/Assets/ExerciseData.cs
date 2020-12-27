using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ExerciseData 
{
    public string[] aufgaben ;
    public string[] lösungen;
    public int[] schwierigkeitsgrade;
    public int easyCount;
    public int medCount;
    public int hardCount;

    public ExerciseData(string[] aufgaben, string[] lösungen, int[] schwierigkeitsgrade)
    {
        this.aufgaben = aufgaben;
        this.lösungen = lösungen;
        this.schwierigkeitsgrade = schwierigkeitsgrade;
        easyCount = 0;
        medCount = 0;
        hardCount = 0;

        for (int i=0 ; i < aufgaben.Length; i++)
        {
            switch(schwierigkeitsgrade[i])
                {
                case 0:
                    {
                        easyCount++;
                        break;
                    }
                case 1:
                    {
                        medCount++;
                        break;
                    }
                case 2:
                    {
                        hardCount++;
                        break;
                    }
                default:
                    {
                        Debug.LogError("not valid difficulty: " + schwierigkeitsgrade[i]);
                        break;
                    }

            }
        }
    }

}
