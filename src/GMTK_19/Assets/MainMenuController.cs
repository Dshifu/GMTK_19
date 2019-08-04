using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Required] [SerializeField] private Animator _mainSceneAnimator = null;
    [Required] [SerializeField] private Animator _fadingAnimator = null;
    public GameObject _fadingGO;

    private bool _isSceneLoading;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        _mainSceneAnimator.SetBool(PrefsName.AnimatorState.StartOpening, true);
    }

    public void StartLoadingGameScene()
    {
        if (_isSceneLoading) return;
        _fadingGO.SetActive(true);
        _fadingAnimator.SetBool(PrefsName.AnimatorState.StartFading, true);
        _isSceneLoading = true;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}
