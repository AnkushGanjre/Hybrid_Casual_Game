using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTask : GAgent
{
    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("GoToRoom", 1, false);
        goals.Add(s1, 3);
    }
}
