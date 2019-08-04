using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [Required]
    [SerializeField] private AudioMixer audioMixer = null;
    [Required]
    [SerializeField] private PanicLevel panicLevel = null;

    [SerializeField] private float distanceToSoundSecondCharacter = 50f;
    [SerializeField] private float distanceToDistraction = 150f;

    public Transform secondCharacter = null;
    public Transform player = null;

    private const float HighVolumeLimit = 0f;
    private const float HighVolumeLimitForDistraction = -30f;
    private const float LowVolumeLimit = -80f;

    private const float CoefForMultiply = 1280f;

    private const float MediumPanicLevel = 0.3f;
    private const float HighPanicLevel = 0.7f;
    private const float MediationPanicLevel = 0.15f;

    private void Start()
    {
        audioMixer.SetFloat(PrefsName.LowPanicVolume, HighVolumeLimit);
        audioMixer.SetFloat(PrefsName.MediumPanicVolume, LowVolumeLimit);
        audioMixer.SetFloat(PrefsName.HighPanicVolume, LowVolumeLimit);
    }

    private void Update()
    {
        SetMusicVolume();
        SetSecondCharacterVolume();
    }

    private void SetSecondCharacterVolume()
    {
        if(secondCharacter == null || player == null)
            return;
        var distance = Vector3.Distance(player.position, secondCharacter.position); 
//        if(distance > distanceToDistraction)
//            return;
        var distractionVolume = -80f;
        var secondCharacterControlVolume = -80f;
        var musicControlVolume = -20f;
        if (distance < distanceToDistraction)
        {
            var coof = (LowVolumeLimit - HighVolumeLimitForDistraction) /
                       (distanceToDistraction - distanceToSoundSecondCharacter);
            var temp = distanceToSoundSecondCharacter * coof - HighVolumeLimitForDistraction;
            distractionVolume = distance * coof - temp;
        }

        if (distance < distanceToSoundSecondCharacter)
        {
            distractionVolume = distance * (HighVolumeLimitForDistraction - LowVolumeLimit) / (distanceToSoundSecondCharacter - 0f) + LowVolumeLimit;
            Debug.Log("coof1: " + (HighVolumeLimitForDistraction - LowVolumeLimit));
            Debug.Log("coof2: " + (distanceToSoundSecondCharacter - 0f));
            Debug.Log("distance: " + distance);
            Debug.Log("distractionVolume: " + distractionVolume);
            musicControlVolume = -distractionVolume *
                                 ((HighVolumeLimit + LowVolumeLimit) /
                                  (-LowVolumeLimit + HighVolumeLimitForDistraction));
            secondCharacterControlVolume = 0f;
        }
        
        audioMixer.SetFloat(PrefsName.DistractionVolume, distractionVolume);
        audioMixer.SetFloat(PrefsName.MusicControlVolume, musicControlVolume);
        audioMixer.SetFloat(PrefsName.SecondCharacterControlVolume, secondCharacterControlVolume);

    }

    private void SetMusicVolume()
    {
        var panicLevelValue = panicLevel.GetPanicLevel;

        var lowPanicVolume = -CoefForMultiply * Mathf.Pow(panicLevelValue, 2f);
        if (panicLevelValue > 0f && panicLevelValue < MediumPanicLevel)
        {
            audioMixer.SetFloat(PrefsName.LowPanicVolume, lowPanicVolume);
        }
        else
        {
            audioMixer.SetFloat(PrefsName.LowPanicVolume, LowVolumeLimit);
        }

        var mediumPanicVolume =
            -CoefForMultiply * Mathf.Pow(panicLevelValue - (MediumPanicLevel + MediationPanicLevel), 2f);
        if (panicLevelValue > MediumPanicLevel - MediationPanicLevel && panicLevelValue < HighPanicLevel)
        {
            audioMixer.SetFloat(PrefsName.MediumPanicVolume, mediumPanicVolume);
        }
        else
        {
            audioMixer.SetFloat(PrefsName.MediumPanicVolume, LowVolumeLimit);
        }

        var highPanicVolume = -CoefForMultiply * Mathf.Pow(panicLevelValue - (HighPanicLevel + MediationPanicLevel), 2f);
        if (panicLevelValue > HighPanicLevel - MediationPanicLevel)
        {
            audioMixer.SetFloat(PrefsName.HighPanicVolume, highPanicVolume);
        }
        else
        {
            audioMixer.SetFloat(PrefsName.HighPanicVolume, LowVolumeLimit);
        }

        var masterVolumeLow = -80f;
        var masterVolumeMedium = -80f;
        var masterVolumeHigh = -80f;
        if (lowPanicVolume < 0 && lowPanicVolume > -80f)
            masterVolumeLow = lowPanicVolume;
        if (mediumPanicVolume < 0 && mediumPanicVolume > -80f)
            masterVolumeMedium = mediumPanicVolume;
        if (highPanicVolume < 0 && highPanicVolume > -80f)
            masterVolumeHigh = highPanicVolume;

        var temp = Mathf.Max(Mathf.Max(masterVolumeLow, masterVolumeMedium), masterVolumeHigh);
        if (Math.Abs(temp - 80f) < 0.1f)
            temp = 0f;
        audioMixer.SetFloat(PrefsName.MusicVolume, -temp);
    }
}
