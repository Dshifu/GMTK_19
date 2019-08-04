using UnityEngine;

public class PanicBarParticleController : MonoBehaviour
{
    private ParticleSystem _barSystem;
    public GameObject PanicBar;
    public PanicLevel PanicLevel;

    void Start()
    {
        _barSystem = GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        if(PanicLevel.IsFullPanicAtTheMoment)
            _barSystem.Stop();
        else
            _barSystem.Play();

        var emmision = _barSystem.emission;
        emmision.rateOverTimeMultiplier = PanicBar.transform.localScale.x * 1000f;
    }
}
