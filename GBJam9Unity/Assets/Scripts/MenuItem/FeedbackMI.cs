using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackMI : MenuItemBase
{
    public eLittleFeedback Feedback = eLittleFeedback.none;

    public override bool PerformAction()
    {
        return true;
    }
}
