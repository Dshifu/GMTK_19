using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Collider2D), typeof(Image))]
public class PanicEnvironmentEffect : MonoBehaviour
{
    [SerializeField] private Sprite textureWithoutStroke = null;
//    public bool isPanicReducer = false;
//
//    private bool IsShowPanicReducer => isPanicReducer;
//    private bool IsShowTimeForFullPanic => !isPanicReducer;

    //[ShowIf("IsShowPanicReducer", true, true), PropertyRange(0f,1f)]
    [PropertyRange(0f,1f)]
    public float reducer;
    
    //[ShowIf("IsShowTimeForFullPanic", false, true)]
    public float timeForFullPanic;

    public void CollectEnvironmentReducer()
    {
        var myImage = GetComponent<Image>();
        myImage.sprite = textureWithoutStroke;
    }
}
