using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will contain all the dialogue text that will be displayed for a dialogue event. This will also contain references to the character
/// properties.
/// </summary>
public class Dialogue : MonoBehaviour {
    #region const variables
    public const string DialogueActionButton = "Submit";
    #endregion const variables

    [SerializeField]
    [Tooltip("This is where you can write all text that will be played out for a Dialogue event")]
    private DialogueSentence[] dialogueSentences;

    /// <summary>
    /// Converts our dialogue sentence array to a queue that can be used by our dialogue system manager
    /// </summary>
    /// <returns></returns>
    public Queue<DialogueSentence> GetDialogueSentences()
    {
        Queue<DialogueSentence> dialogueQueue = new Queue<DialogueSentence>();
        foreach (DialogueSentence dialogueSentence in dialogueSentences)
        {
            dialogueQueue.Enqueue(dialogueSentence);
        }
        return dialogueQueue;
    }


    [System.Serializable]
    public class DialogueSentence
    {
        [Tooltip("A reference to all the properties the character that is speaking will contain")]
        public DialogueCharacterProperties characterProperties;
        [Tooltip("This will be the sentence that will be written in our dialogue box")]
        [TextArea(3, 10)]
        public string sentence;      
    }
}
