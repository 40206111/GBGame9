using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToMage : MonoBehaviour, IInteractable
{
    bool Visited;

    public void RunInteraction()
    {
        ConversationWithCauldron();
    }

     void ConversationWithCauldron()
    {
        if (!Visited)
        {
            DialogueBoxControl.Instance.PrintTextWithCharacter("Can I... help you?", eSpeakingCharacter.Mage, leftSide: true, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("What's going on?", eSpeakingCharacter.Melee, leftSide: false, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("A strange chicken walks into my house uninvited and I'm supposed to be the one that know's what's going on?", eSpeakingCharacter.Mage, leftSide: true, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("Well I don't even know where the cluck I am!", eSpeakingCharacter.Melee, leftSide: false, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("...my house?", eSpeakingCharacter.Mage, leftSide: true, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("... ... ... Can you... help me work out what's going on?", eSpeakingCharacter.Melee, leftSide: false, closeAfterText: true);
            Visited = true;
        }
        DialogueBoxControl.Instance.QueueDialogue("I'd love to help, but I couldn't possibly leave my cauldron alone.", eSpeakingCharacter.Mage, leftSide: true, closeAfterText: true);
    }

}
