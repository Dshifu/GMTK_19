using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [Required]
    [SerializeField] private AudioMixer audioMixer = null;
    [Required]
    [SerializeField] private PanicLevel panicLevel = null;

    private const float HighVolumeLimit = 0f;
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
        var panicLevelValue = panicLevel.GetPanicLevel;
        
        var LowPanicVolume = -CoefForMultiply * Mathf.Pow(panicLevelValue, 2f);
        if (panicLevelValue > 0f && panicLevelValue < MediumPanicLevel)
        {
            audioMixer.SetFloat(PrefsName.LowPanicVolume, LowPanicVolume);
        }
        else
        {
            audioMixer.SetFloat(PrefsName.LowPanicVolume, LowVolumeLimit);
        }

//        if(panicLevelValue < 0.24f)
//            audioMixer.SetFloat(PrefsName.MasterVolume, temp);
        
        var MediumPanicVolume = -CoefForMultiply * Mathf.Pow(panicLevelValue - (MediumPanicLevel + MediationPanicLevel), 2f);
        if (panicLevelValue > MediumPanicLevel - MediationPanicLevel && panicLevelValue < HighPanicLevel)
        {
            audioMixer.SetFloat(PrefsName.MediumPanicVolume, MediumPanicVolume);
        }
        else
        {
            audioMixer.SetFloat(PrefsName.MediumPanicVolume, LowVolumeLimit);
        }
        
//        if(panicLevelValue > 0.24f && panicLevelValue < 0.73f)
//            audioMixer.SetFloat(PrefsName.MasterVolume, temp);

        var HighPanicVolume = -CoefForMultiply * Mathf.Pow(panicLevelValue - (HighPanicLevel + MediationPanicLevel), 2f);
        if (panicLevelValue > HighPanicLevel - MediationPanicLevel)
        {
            audioMixer.SetFloat(PrefsName.HighPanicVolume, HighPanicVolume);
        }
        else
        {
            audioMixer.SetFloat(PrefsName.HighPanicVolume, LowVolumeLimit);
        }

        float masterVolumeLow = -80f;
        float masterVolumeMedium = -80f;
        float masterVolumeHigh = -80f;
        if (LowPanicVolume < 0 && LowPanicVolume > -80)
            masterVolumeLow = LowPanicVolume;
        if (MediumPanicVolume < 0 && MediumPanicVolume > -80)
            masterVolumeMedium = MediumPanicVolume;
        if (HighPanicVolume < 0 && HighPanicVolume > -80)
            masterVolumeHigh = HighPanicVolume;

        var temp = Mathf.Max(Mathf.Max(masterVolumeLow, masterVolumeMedium), masterVolumeHigh);
        audioMixer.SetFloat(PrefsName.MasterVolume, -temp);
//        if(panicLevelValue > 0.73f)
//            audioMixer.SetFloat(PrefsName.MasterVolume, temp);
    }
}
