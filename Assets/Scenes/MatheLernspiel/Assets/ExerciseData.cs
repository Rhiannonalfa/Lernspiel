using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ExerciseData 
{
    // public Exercise[] exercises; // GEH SCHEISSEN ALEX
    public List<Exercise> exercises;
    public int easyCount;
    public int medCount;
    public int hardCount;

    public ExerciseData(List<Exercise> exercises)
    {
        this.exercises = exercises;
        easyCount = 0;
        medCount = 0;
        hardCount = 0;

        foreach(Exercise exercise in exercises)
        {
            switch(exercise.schwierigkeitsgrad)
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
                        Debug.LogError("not valid difficulty: " + exercise.schwierigkeitsgrad);
                        break;
                    }
            }
        }
    }
}