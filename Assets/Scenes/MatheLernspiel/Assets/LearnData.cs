using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LearnData 
{
    public List<Learncontent> learncontents;
    
    
    public LearnData(List <Learncontent> learncontents)
    {
        this.learncontents = learncontents;
    }
}
