using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approach : GAction
{
    public override bool PrePerform()
    {

        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }

    public override float CalculateUtility()
    {
        float currentUtilityScore = 0.5f;



        SetCurrentUtilityScore(currentUtilityScore);
        return currentUtilityScore;
    }

    // ?????? SetCurrentUtilityScore ??????, ???? ???????? ?????? ????????.
    public void SetCurrentUtilityScore(float score)
    {
        CurrentUtilityScore = score;
    }


}
