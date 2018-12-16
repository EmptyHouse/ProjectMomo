using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour {
    #region static variables
    private static DialogueUIManager instance;

    public static DialogueUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject.FindObjectOfType<DialogueUIManager>();
            }
            return instance;
        }
    }
    #endregion static variables

    [Header("Dialogue Settings")]
    [Tooltip("The number of seconds we will wait before drawing more text")]
    public float secondsBeforeDrawingNextLetter = .05f;

    [Header("UI References")]
    private DialogueNPC npcThatIsBeingTalkedTo;
    public Transform dialogueContainer;
    public Text dialogueTextBox;
    public Queue<Dialogue.DialogueSentence> dialogueSentenceQueue;

    #region monobehaviour methods
    private void Awake()
    {
        instance = this;
        dialogueContainer.gameObject.SetActive(false);
    }
    #endregion monobehaivour methods


    public void StartDialogue(Dialogue dialogueEvent, DialogueNPC dialogueNPC)
    {
        if (dialogueEvent == null)
        {
            return;
        }
        npcThatIsBeingTalkedTo = dialogueNPC;
        dialogueContainer.gameObject.SetActive(true);
        GameOverseer.Instance.playerCharacterStats.playerController.enabled = false;

        dialogueSentenceQueue = dialogueEvent.GetDialogueSentences();
        StartNextDialogueSentence();
    }

    private void StartNextDialogueSentence()
    {
        if (dialogueSentenceQueue.Count > 0)
        {
            StartCoroutine(DrawDialogueTextToTextBox(dialogueSentenceQueue.Dequeue()));
            return;
        }
        CloseDialogue();
    }

    
    /// <summary>
    /// Closes the Dialogue UI and gives control back to our player
    /// </summary>
    public void CloseDialogue()
    {
        dialogueContainer.gameObject.SetActive(false);
        GameOverseer.Instance.playerCharacterStats.playerController.enabled = true;
        npcThatIsBeingTalkedTo.StartCoroutine(npcThatIsBeingTalkedTo.DelayBeforeAllowingPlayerToEnterDialogue());
    }

    private IEnumerator DrawDialogueTextToTextBox(Dialogue.DialogueSentence dialogueSentenceToDraw)
    {
        float timeThatHasPassed = 0;
        string dialogueText = "";
        dialogueTextBox.text = "";
        int textIndex = 0;
        while (dialogueText != dialogueSentenceToDraw.sentence)
        {
            if (timeThatHasPassed >= secondsBeforeDrawingNextLetter)
            {
                timeThatHasPassed = 0;
                ++textIndex;
                dialogueText = dialogueSentenceToDraw.sentence.Substring(0, textIndex);
                dialogueTextBox.text = dialogueText;
            }
            timeThatHasPassed += CustomTime.DeltaTime;
            yield return null;
            if (Input.GetButtonDown("Submit"))
            {
                dialogueText = dialogueSentenceToDraw.sentence;
                dialogueTextBox.text = dialogueText;
                yield return null;
            }
        
        }
        while (!Input.GetButtonDown("Submit"))
        {
            yield return null;
        }
        StartNextDialogueSentence();
    }
}
