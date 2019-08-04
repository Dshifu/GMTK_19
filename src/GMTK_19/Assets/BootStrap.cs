using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [Required] [SerializeField] private Animator _fadingAnimator = null;
    [Required] [SerializeField] private LevelGenerator _levelGenerator = null;

    [SerializeField] private PanicLevel panicLevel = null;
    [SerializeField] private AudioMixerController audioMixerController = null;
    [SerializeField] private CameraFollow cameraFollow = null;

    IEnumerator Start()
    {
        _levelGenerator.gameObject.SetActive(true);
        yield return null;
        yield return null;
        yield return null;
        var player = GameObject.FindGameObjectWithTag("Player");
        cameraFollow.followTarget = player.transform;
        player.GetComponent<PanicEnvironmentEffectController>().panicLevel =
            panicLevel;
        audioMixerController.secondCharacter = GameObject.FindGameObjectWithTag("SecondCharacter").transform;
        _fadingAnimator.SetBool(PrefsName.AnimatorState.StartUnFading, true);
    }
}
