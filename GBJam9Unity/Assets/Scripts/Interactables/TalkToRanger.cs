using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToRanger : MonoBehaviour, IInteractable
{

    public void RunInteraction()
    {
        Talking();
        PlayerData.KeyEvents |= eKeyEvents.MetRanger;
        PlayerData.SetChickenAvailable(eChickenClass.ranger, true);
    }

    void Talking()
    {
        DialogueBoxControl.Instance.PrintTextWithCharacter("Woah! hey! I was hiding in there!, I only just found somewhere safe in this strange place to hide.", eSpeakingCharacter.Range, leftSide: true, closeAfterText: true);
        DialogueBoxControl.Instance.QueueDialogue("Oh sorry. I though you were stuck.", eSpeakingCharacter.Melee, leftSide: false, closeAfterText: true);
        DialogueBoxControl.Instance.QueueDialogue("Well... uhh.. I am stuck... in a way... I don't know where I am... I just sort of... woke up here.", eSpeakingCharacter.Range, leftSide: true, closeAfterText: true);
        DialogueBoxControl.Instance.QueueDialogue("Oh! Same! You should come with me. I'm going to find whoever did this to us and cluck them up!", eSpeakingCharacter.Melee, leftSide: false, closeAfterText: true);
        DialogueBoxControl.Instance.QueueDialogue("..I'm afraid I'm a bit of a ch-ch-chicken. But I will come with you... Nothing bad could possibly get past your fire.", eSpeakingCharacter.Range, leftSide: true, closeAfterText: true);
        DialogueBoxControl.Instance.QueueDialogue("*A new Chicken has joined your party! Press select to change chicken, or start to view your chicken's equiptment*", eSpeakingCharacter.None, leftSide: false, closeAfterText: true);
        StartCoroutine(WaitForConversation());
    }

    IEnumerator WaitForConversation()
    {
        while (DialogueBoxControl.Instance.QueueCount > 0)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
