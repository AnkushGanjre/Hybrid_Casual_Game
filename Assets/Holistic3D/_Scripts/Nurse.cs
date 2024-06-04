using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("treatPatient", 1, false);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal("rested", 1, true);
        goals.Add(s2, 5);

        Invoke(nameof(GetTired), Random.Range(10, 30));
    }

    void GetTired()
    {
        beliefs.ModifyState("exhausted", 0);
        Invoke(nameof(GetTired), Random.Range(10, 30));
    }
}
