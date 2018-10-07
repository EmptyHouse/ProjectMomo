using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Audio Manager script. This should be used for handling the audio settings in game
/// </summary>
public class AudioManagement : MonoBehaviour {
    #region const variables
    public const float MINIMUM_VOLUME_DECIBLES = -80f;
    #endregion const variables
    #region static variables
    private static AudioManagement instance;

    public static AudioManagement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AudioManagement>();
                if (instance == null)
                {
                    Debug.LogWarning("There is no AudioManagement script found in the hierarchy. Please make sure to add one in.");
                }
            }

            return instance;
        }
    }
    #endregion static variables

    #region monobehaviour methods
    private void Awake()
    {
    }
    #endregion monobehaviour methods


    #region static methods

    /// <summary>
    /// Due to audio in unity not being calculated linearly we have to conver the value to decibels first
    /// and then scale the volume from there. This will take a float value between 0 and 1
    /// </summary>
    /// <returns></returns>
    public static float AdjustPerceivedVolume(float expectedVolume)
    {
        if (expectedVolume == 0)
        {
            return 0;
        }
        float db = MINIMUM_VOLUME_DECIBLES - (expectedVolume * MINIMUM_VOLUME_DECIBLES);

        return Mathf.Pow(10f, db / 20f);
    }
    #endregion static methods

}
