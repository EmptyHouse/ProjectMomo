using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {
    [Tooltip("This is where you can write all text that will be played out for a Dialogue event")]
    public DialogueSentences[] dialogueSentences;


    [System.Serializable]
    public class DialogueSentences
    {
        [Tooltip("A reference to all the properties the character that is speaking will contain")]
        public DialogueCharacterProperties characterProperties;
        [Tooltip("This will be the sentence that will be written in our dialogue box")]
        [TextArea(3, 10)]
        public string sentence;      
    }
}
