using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class PanicEnvironmentEffect : MonoBehaviour
{
    [SerializeField] private Sprite textureWithoutStroke = null;
//    public bool isPanicReducer = false;
//
//    private bool IsShowPanicReducer => isPanicReducer;
//    private bool IsShowTimeForFullPanic => !isPanicReducer;

    //[ShowIf("IsShowPanicReducer", true, true), PropertyRange(0f,1f)]
    [PropertyRange(-1f,1f)]
    public float reducer;
    
    //[ShowIf("IsShowTimeForFullPanic", false, true)]
    public float timeForFullPanic;

    public void CollectEnvironmentReducer()
    {
        foreach (var collider in GetComponentsInChildren<Collider2D>())
            collider.enabled = false;

        var myImage = GetComponent<SpriteRenderer>();
        if(textureWithoutStroke != null)
            myImage.sprite = textureWithoutStroke;
        myImage.color = new Color(1f,1f,1f,0.5f);
        reducer = 0f;
    }
}
