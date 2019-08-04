using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [Required] [SerializeField] private Animator _fadingAnimator = null;
    [Required] [SerializeField] private LevelGenerator _levelGenerator = null;

    IEnumerator Start()
    {
        _levelGenerator.gameObject.SetActive(true);
        yield return null;
        _fadingAnimator.SetBool(PrefsName.AnimatorState.StartUnFading, true);
    }
}
