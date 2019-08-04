using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Required] [SerializeField] private Animator _mainSceneAnimator = null;

    public AnimationClip StartGameAnimation;
    public GameObject StartBtn;
    public GameObject FadingGo;

    private bool _isSceneLoading;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        _mainSceneAnimator.SetBool(PrefsName.AnimatorState.StartOpening, true);
    }

    public void StartLoadingGameScene()
    {
        _mainSceneAnimator.SetBool(PrefsName.AnimatorState.StartOpening, false);
        if (_isSceneLoading) return;
        FadingGo.SetActive(true);
        StartBtn.SetActive(false);
        _mainSceneAnimator.SetBool(PrefsName.AnimatorState.StartPlay, true);
        _isSceneLoading = true;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(StartGameAnimation.length + 2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}
