using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(CharacterMovementController))]
public class MovementBonusEffectController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.CompareTag(PrefsName.MovementBonus)) return;
        var bonusEffect = other.GetComponent<MovementBonusEffect>();
        transform.GetComponent<CharacterMovementController>().SetMovementBonus(bonusEffect.movementBonusSettings);
        bonusEffect.CollectMovementBonus();
    }
}
