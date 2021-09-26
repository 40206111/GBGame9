using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour, IInteractable
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    Collider2D BoxCollider;
    [SerializeField]
    List<ItemDetails> ItemsThatCut;

    public void RunInteraction()
    {
        //if player has equipped item that cuts on current chicken
        {
            animator.SetTrigger("Cut");
            BoxCollider.enabled = false;

            int random = Random.Range(0, 2);
            if (random != 0)
            {
                string addS = random == 1 ? "" : "s";
                PrintToDialogue($"Ooh {random} Sunflower Seed{addS}!");
                PlayerData.Seeds += random;
            }

        }
        //else
        {
            PrintToDialogue("I reckon I could cut that down... if I had the right tool");
        }
    }

    private void PrintToDialogue(string txt)
    {
        DialogueBoxControl.Instance.PrintText(txt, closeAfterText: true);
    }
}
