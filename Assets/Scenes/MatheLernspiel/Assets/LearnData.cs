using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnData : IComparable
{
    public List<Learncontent> learncontents;
    public string theme;
    public int number;
    public bool exerciseSet = false;
    public ExerciseData exerciseData;
    
    
    public LearnData(List <Learncontent> learncontents, string theme)
    {
        this.learncontents = learncontents;
        this.theme = theme;
    }

    public int CompareTo(object other)
    {
        if (other == null) return 1;
        LearnData otherLearnData = other as LearnData;
        if (otherLearnData == null) return 1;
        return this.number.CompareTo(otherLearnData.number);
    }
}
