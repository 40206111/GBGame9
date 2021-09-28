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
        if (CanCut())
        {
            animator.SetTrigger("Cut");
            BoxCollider.enabled = false;

            int random = Random.Range(0, 3);
            if (random != 0)
            {
                string addS = random == 1 ? "" : "s";
                PrintToDialogue($"Ooh {random} Sunflower Seed{addS}!");
                PlayerData.Seeds += random;
            }

        }
        else
        {
            PrintToDialogue("I reckon I could cut that down... if I had the right tool.");
        }
    }

    private bool CanCut()
    {
        bool outBool = false;
        foreach(EquiptmentSlot es in PlayerData.GetChickenData(PlayerData.ActiveChicken).EquippedItems.GetAllEquipped())
        {
            if (ItemsThatCut.Contains(es.Equiptment))
            {
                outBool = true;
                break;
            }
        }

        return outBool;
    }

    private void PrintToDialogue(string txt)
    {
        DialogueBoxControl.Instance.PrintText(txt, closeAfterText: true);
    }
}
