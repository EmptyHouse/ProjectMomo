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
    public Dialogue testDialogue;
    public Transform dialogueContainer;
    public Text dialogueTextBox;
    public Queue<Dialogue.DialogueSentence> dialogueSentenceQueue;

    #region monobehaviour methods
    private void Awake()
    {
        instance = this;
        dialogueContainer.gameObject.SetActive(false);
        StartDialogue(testDialogue);
    }
    #endregion monobehaivour methods


    public void StartDialogue(Dialogue dialogueEvent)
    {
        if (dialogueEvent == null)
        {
            return;
        }
        dialogueContainer.gameObject.SetActive(true);
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

    

    public void CloseDialogue()
    {
        dialogueContainer.gameObject.SetActive(false);
    }

    private IEnumerator DrawDialogueTextToTextBox(Dialogue.DialogueSentence dialogueSentenceToDraw)
    {
        float timeThatHasPassed = 0;
        string dialogueText = "";
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
