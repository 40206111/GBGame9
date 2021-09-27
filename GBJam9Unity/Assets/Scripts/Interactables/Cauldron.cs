using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable
{
    [SerializeField]
    ItemDetails WhatsInThePot;
    [SerializeField]
    Animator Animator;
    [SerializeField]
    GameObject Mage;
    [SerializeField]
    Collider2D BoxCollder;


    public void RunInteraction()
    {
        Animator.SetTrigger("Cut");
        BoxCollder.enabled = false;
        PlayerData.KeyEvents |= eKeyEvents.MetMage;
        StartCoroutine(TalkToMage());
        PlayerData.SetChickenAvailable(eChickenClass.mage, true);
    }

    IEnumerator TalkToMage()
    {
        yield return new WaitForSeconds(1.0f);

        DialogueBoxControl.Instance.PrintTextWithCharacter("Did you just break my cauldron?! Why would you do that?!", eSpeakingCharacter.Mage, leftSide: true, closeAfterText: true);
        DialogueBoxControl.Instance.QueueDialogue("...I... uh...", eSpeakingCharacter.Melee, leftSide: false, closeAfterText: true);
        DialogueBoxControl.Instance.QueueDialogue("Just so I'd help you???? Well you're darn right I'm coming with you now! I'll be by your side until you have found me a brand new cauldron!", eSpeakingCharacter.Mage, leftSide: true, closeAfterText: true);
        DialogueBoxControl.Instance.QueueDialogue("You don't have to...", eSpeakingCharacter.Melee, leftSide: false, closeAfterText: true);
        DialogueBoxControl.Instance.QueueDialogue("Too late! I'm coming with you!", eSpeakingCharacter.Mage, leftSide: true, closeAfterText: true);
        DialogueBoxControl.Instance.QueueDialogue("*A new Chicken has joined your party! Press select to change chicken, or start to view your chicken's equiptment*", eSpeakingCharacter.None, leftSide: false, closeAfterText: true);
        StartCoroutine(WaitForConversation());
    }

    IEnumerator WaitForConversation()
    {
        while (DialogueBoxControl.Instance.QueueCount > 0)
        {
            yield return null;
        }

        Mage.SetActive(false);
    }
}
