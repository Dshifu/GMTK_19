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

    private float CoefForMultiplyForLow = 0f;
    private float CoefForMultiplyForMedium = 0f;
    private float CoefForMultiplyForHard = 0f;

    private const float MediumPanicLevel = 0.3f;
    private const float HighPanicLevel = 0.8f;
    private const float MediationPanicLevel = 0.15f;

    private void Start()
    {
        audioMixer.SetFloat(PrefsName.LowPanicVolume, HighVolumeLimit);
        audioMixer.SetFloat(PrefsName.MediumPanicVolume, LowVolumeLimit);
        audioMixer.SetFloat(PrefsName.HighPanicVolume, LowVolumeLimit);
        CoefForMultiplyForLow = -LowVolumeLimit / Mathf.Pow(MediumPanicLevel, 2f);
        CoefForMultiplyForMedium = -LowVolumeLimit / Mathf.Pow(-MediationPanicLevel - (HighPanicLevel-MediumPanicLevel)/2, 2f);
        CoefForMultiplyForHard = -LowVolumeLimit / Mathf.Pow(-MediationPanicLevel - (1f - HighPanicLevel)/2, 2f);
    }

    private void Update()
    {
        var panicLevelValue = panicLevel.GetPanicLevel;
        var temp = CoefForMultiplyForLow * Mathf.Pow(panicLevelValue, 2f);
        if (panicLevelValue > 0f && panicLevelValue < MediumPanicLevel)
        {
            
            audioMixer.SetFloat(PrefsName.LowPanicVolume, -temp);
            audioMixer.SetFloat(PrefsName.MasterVolume, temp);
        }
        else
        {
            audioMixer.SetFloat(PrefsName.LowPanicVolume, LowVolumeLimit);
        }
        
        if(panicLevelValue < 0.24f)
            audioMixer.SetFloat(PrefsName.MasterVolume, temp);

        if (panicLevelValue > MediumPanicLevel - MediationPanicLevel && panicLevelValue < HighPanicLevel)
        {
            temp = CoefForMultiplyForMedium * Mathf.Pow(panicLevelValue - (MediumPanicLevel + MediationPanicLevel), 2f);
            audioMixer.SetFloat(PrefsName.MediumPanicVolume, -temp);
        }
        else
        {
            audioMixer.SetFloat(PrefsName.MediumPanicVolume, LowVolumeLimit);
        }
        
        if(panicLevelValue > 0.24f && panicLevelValue < 0.73f)
            audioMixer.SetFloat(PrefsName.MasterVolume, temp);

        if (panicLevelValue > HighPanicLevel - MediationPanicLevel)
        {
            temp = CoefForMultiplyForHard * Mathf.Pow(panicLevelValue - (HighPanicLevel + MediationPanicLevel), 2f);
            audioMixer.SetFloat(PrefsName.HighPanicVolume, -temp);
        }
        else
        {
            audioMixer.SetFloat(PrefsName.HighPanicVolume, LowVolumeLimit);
        }
        if(panicLevelValue > 0.73f)
            audioMixer.SetFloat(PrefsName.MasterVolume, temp);
    }
}
