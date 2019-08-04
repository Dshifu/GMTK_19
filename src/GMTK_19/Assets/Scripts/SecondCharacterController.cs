using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCharacterController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.CompareTag(PrefsName.SecondCharacter)) return;
        GameController.Instance.GameOver(true);
    }
}
