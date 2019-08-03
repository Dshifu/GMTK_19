using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MovementBonusEffect : MonoBehaviour
{
    [HideLabel]
    public CharacterMovementController.MovementBonusSettings movementBonusSettings = new CharacterMovementController.MovementBonusSettings();

    private void Start()
    {
        if (movementBonusSettings.movementBonusType == CharacterMovementController.MovementBonusSettings.MovementBonusType.Shampoo)
            movementBonusSettings.delayBeforeActivation = Random.Range(1f, 3f);
    }

    public void CollectMovementBonus()
    {
        transform.gameObject.SetActive(false);
    }
}
