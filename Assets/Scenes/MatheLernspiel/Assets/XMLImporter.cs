using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLImporter : MonoBehaviour
{
    public static LearnData[] learnData;
    public static ExerciseData[] exerciseData;



    private void Awake()
    {
        learnData = new LearnData[] { new LearnData(new string[] { "aaaaaaaaaaaa", "bbbbbbbbbbbbbbbb", "cccccccccccc" }) };
        exerciseData = new ExerciseData[] { new ExerciseData(new string[] { "AAAAAAAAA", "BBBBBBBBBBBBBB", "CCCCCCCCCCCCCCCC" }, new string[] { "1", "2", "3" }, new int[] { 0, 0, 0 })};
    }
}
 