using UnityEngine;

public class PanicBarParticleController : MonoBehaviour
{
    private ParticleSystem _barSystem;
    public GameObject PanicBar;
    public PanicLevel PanicLevel;

    private float pixelWidth;

    void Start()
    {
        _barSystem = GetComponent<ParticleSystem>();
        pixelWidth = Camera.main.pixelWidth;

        var shape = _barSystem.shape;
        shape.scale = new Vector3( (pixelWidth / 2f) - 50f, 1f,1f);
    }

    private void LateUpdate()
    {
        if (PanicLevel.IsFullPanicAtTheMoment)
            _barSystem.Stop();
        else
            _barSystem.Play();

        var emission = _barSystem.emission;
        emission.rateOverTimeMultiplier = PanicBar.transform.localScale.x * 1000f;
    }
}
