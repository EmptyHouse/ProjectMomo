using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that handles text that will appear above characters when they take damage or heal
/// </summary>
public class CharacterDamageText : MonoBehaviour
{ 
    public enum DamageTextType
    {
        HealingText,
        DamageTakenText,
    }

    private DamageTextType damageTextType;
    public Text damageText;

    public Color damageTextColor;
    public Color healingTextColor;
    public float speedOfText = 10f;
    public float minRotation = 45f;
    public float maxRotation = 135f;
    [Tooltip("The amount of time that our text object will remain on screen before despawaning")]
    public float damageTextLifetime = 1;
    public float fadeOutTime = .5f;

    private Vector2 damageTextVector;

    #region monobehaviour methods

    private void OnValidate()
    {
        if (fadeOutTime > damageTextLifetime)
        {
            fadeOutTime = damageTextLifetime;
        }
    }
    #endregion monobehaviour methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeOfText"></param>
    /// <param name="valueToDisplay"></param>
    public void SetupDamageText(DamageTextType typeOfText, float valueToDisplay, Vector3 originPoint)
    {
        transform.localPosition = originPoint;
        damageText.text = Mathf.Round(Mathf.Abs(valueToDisplay)).ToString();

        switch(typeOfText)
        {
            case DamageTextType.DamageTakenText:
                damageText.color = damageTextColor;
                StartCoroutine(RunThroughDamageTextRoutine(Random.Range(minRotation, maxRotation)));
                return;
            case DamageTextType.HealingText:
                damageText.color = healingTextColor;
                StartCoroutine(RunThroughDamageTextRoutine(Random.Range(minRotation, maxRotation)));//This will probably be a slightly different coroutine when adding health, since it will probably happen less often than taking damage
                return;
        }
    }

    /// <summary>
    /// Coroutine that runs through the Damage text sequence. This will fade out the text and despawn the object upon completion
    /// </summary>
    /// <param name="degreesToSendDamageText"></param>
    /// <returns></returns>
    private IEnumerator RunThroughDamageTextRoutine(float degreesToSendDamageText)
    {

        Vector2 directionToMoveText = new Vector2(Mathf.Cos(degreesToSendDamageText * Mathf.Deg2Rad), Mathf.Sin(degreesToSendDamageText * Mathf.Deg2Rad));
        directionToMoveText = directionToMoveText.normalized;
        float timeRemainingBeforeBeginFadeout = damageTextLifetime - fadeOutTime;
        while (timeRemainingBeforeBeginFadeout > 0)
        {
            timeRemainingBeforeBeginFadeout -= CustomTime.DeltaTime;
            TranslateText(directionToMoveText);
            yield return null;
        } 

        float timeRemainingFadeOut = fadeOutTime;
        while (timeRemainingFadeOut > 0)
        {
            timeRemainingFadeOut -= CustomTime.DeltaTime;
            TranslateText(directionToMoveText);
            damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, timeRemainingFadeOut / fadeOutTime);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void TranslateText(Vector2 directionToMoveText)
    {
        Vector2 offset = speedOfText * directionToMoveText * CustomTime.DeltaTime;
        transform.position = transform.position + new Vector3(offset.x, offset.y);
    }
}
