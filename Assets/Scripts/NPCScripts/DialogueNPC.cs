using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour {
    private const float DELAY_BEFORE_REENTER_DIALOGUE = .5f;
    public bool lookAtPlayer = false;
    public Dialogue npcDialogue;
    private PlayerCharacterStats playerStatsThatEntered;
    public bool isCurrentlyInDialogue { get; set; }
    private Animator anim;
    private SpriteRenderer npcSprite;

    private void Awake()
    {
        npcSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerStatsThatEntered && !isCurrentlyInDialogue)
        {
            if (Input.GetButtonDown(Dialogue.DialogueActionButton))
            {
                isCurrentlyInDialogue = true;
                DialogueUIManager.Instance.StartDialogue(npcDialogue, this);
            }
        }
        if (lookAtPlayer)
        {
            if (GameOverseer.Instance.playerCharacterStats.transform.position.x - this.transform.position.x >= 0)
            {
                if (npcSprite.flipX)
                {
                    npcSprite.flipX = false;
                }
            }
            else
            {
                if (!npcSprite.flipX)
                {
                    npcSprite.flipX = true;
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCharacterStats playerStats = collision.GetComponent<PlayerCharacterStats>();
        if (playerStats)
        {
            playerStatsThatEntered = playerStats;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacterStats>())
        {
            playerStatsThatEntered = null;
        }
    }

    public IEnumerator DelayBeforeAllowingPlayerToEnterDialogue()
    {
        yield return new WaitForSeconds(DELAY_BEFORE_REENTER_DIALOGUE);
        isCurrentlyInDialogue = false;
    }
}
