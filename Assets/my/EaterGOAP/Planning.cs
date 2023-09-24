using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planning : GAgent
{
    protected override void Start()
    {
        base.Start();
        //SubGoal s1 = new SubGoal(goal1, 1, true);
        //goals.Add(s1, 3);

        //SubGoal s2 = new SubGoal(goal2, 1, true);
        //goals.Add(s2, 3);
    }

    public void CreateSubGoal(string g1)
    {
        SubGoal s1 = new SubGoal(g1, 1, true);
        goals.Add(s1, 1);
    }

    public void CreateSubGoals(string g1, string g2)
    {
        SubGoal s1 = new SubGoal(g1, 1, true);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal(g2, 1, true);
        goals.Add(s2, 3);
    }
  
}
