using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadThingsForGame : MonoBehaviour
{
    [SerializeField]
    string WakeUpText;


    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.Instance.Shop == null)
        {
            SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
        }
    }

    private void Start()
    {
        if (!PlayerData.KeyEvents.HasFlag(eKeyEvents.WokenUp))
        {
            PlayerData.KeyEvents |= eKeyEvents.WokenUp;


            DialogueBoxControl.Instance.QueueDialogue(WakeUpText, eSpeakingCharacter.Melee, leftSide: true, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("It appears to be some sort of meadow. Or maybe a grassland...", eSpeakingCharacter.Melee, leftSide: true, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("I don't know! I'm a fighter, not an anthropologist!", eSpeakingCharacter.Melee, leftSide: true, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("And why am I even TALKING about PLANTS?! I'm away from my post!", eSpeakingCharacter.Melee, leftSide: true, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("  \"You have the duty of guarding the essence of our very lives. The duty of protecting our magical origins is a high honour, and very important! So don't you dare leave until you are told you can leave.\"", eSpeakingCharacter.Melee, leftSide: true, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("I've heard that nearly every day on the job... She'll be so disappointed...", eSpeakingCharacter.Melee, leftSide: true, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("...", eSpeakingCharacter.Melee, leftSide: true, closeAfterText: true);
            DialogueBoxControl.Instance.QueueDialogue("Oh, a nest! Wonder if it contains anything interesting...", eSpeakingCharacter.Melee, leftSide: true, closeAfterText: true);
        }
    }

}
