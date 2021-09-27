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
        }
    }

}
