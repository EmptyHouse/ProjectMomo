using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Logic that relates to a character performing combat related mechanics should be
/// located in this script
/// </summary>
public class CombatMechanics : MonoBehaviour {
    #region const variables
    private const string PROJECTILE_ANIMATION_TRIGGER = "ProjectileTrigger";
    private const float TIME_TO_BUFFER = 5f * (1f / 60f);
    #endregion const variables

    public enum AttackTypes
    {
        Projectile,//These are very basic, but perhaps in the future we can expand on these
        Melee,
    }

    #region main variables
    private Animator anim;
    private Dictionary<AttackTypes, float> bufferInputTimer = new Dictionary<AttackTypes, float>();

    #endregion main variables

    #region monobehaviour methods
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    #endregion monobehaviour methods

    #region projectile based combat
    /// <summary>
    /// Call this method to begin the animation for firing our arrow
    /// </summary>
    public void FireArrowAnimation()
    {
        anim.SetTrigger(PROJECTILE_ANIMATION_TRIGGER);
    }
    #endregion projectile based combat


    /// <summary>
    /// This coroutine is used to buffer certain combat commands. This will allow the player seem leniency when trying to read in their
    /// inputs so that it does not require perfect timing. For example a melee combo attack would be very difficult if it required perfect timing
    /// But would also feel unresponsive if the player did not intendo for the 2nd or 3rd hit in the combo to come through.
    /// </summary>
    /// <returns></returns>
    private IEnumerator BufferCombatAnimationInput(AttackTypes attackTypeToBuffer)
    {
        if (bufferInputTimer[attackTypeToBuffer] > 0)
        {
            yield break;
        }
        bufferInputTimer[attackTypeToBuffer] = TIME_TO_BUFFER;

        while (bufferInputTimer[attackTypeToBuffer] > 0)
        {
            yield return null;
            bufferInputTimer[attackTypeToBuffer] -= Time.deltaTime;
        }
        anim.ResetTrigger(PROJECTILE_ANIMATION_TRIGGER);
    }
}
