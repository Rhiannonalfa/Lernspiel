using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLImporter : MonoBehaviour
{
    public static LearnData[] learnData;
    


    private void Awake()
    {
        learnData = new LearnData[]{ new LearnData(new string[] { "aaaaaaaaaaaa", "bbbbbbbbbbbbbbbb", "cccccccccccc" })};
    }
}
