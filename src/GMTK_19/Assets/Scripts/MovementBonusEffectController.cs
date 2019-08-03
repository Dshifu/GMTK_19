using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(CharacterMovementController))]
public class MovementBonusEffectController : MonoBehaviour
{
    private CharacterMovementController characterMovementController;
    
    private CharacterMovementController.MovementBonusSettings movementBonusSettings = null;

    private void Start()
    {
        characterMovementController = GetComponent<CharacterMovementController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.CompareTag(PrefsName.MovementBonus)) return;
        var bonusEffect = other.GetComponent<MovementBonusEffect>();
        movementBonusSettings = bonusEffect.movementBonusSettings;
        bonusEffect.CollectMovementBonus();
    }

    private void FixedUpdate()
    {
        if(movementBonusSettings == null)
            return;
        if (movementBonusSettings.effectiveTime < 0)
        {
            characterMovementController.isMovementBonus = false;
            characterMovementController.speedBonusMultiply = 1f;
        }
        
        if(Input.GetAxisRaw("Vertical") < 1f)
            return;

        switch (movementBonusSettings.movementBonusType)
        {
            case CharacterMovementController.MovementBonusSettings.MovementBonusType.AK:
            case CharacterMovementController.MovementBonusSettings.MovementBonusType.BEANS:
            case CharacterMovementController.MovementBonusSettings.MovementBonusType.FIRESHIT:
                characterMovementController.speedBonusMultiply = movementBonusSettings.straightSpeedMultiplier;
                movementBonusSettings.effectiveTime -= Time.deltaTime;
                break;
            case CharacterMovementController.MovementBonusSettings.MovementBonusType.SHAMPOO:
            case CharacterMovementController.MovementBonusSettings.MovementBonusType.COKE:
                
                StartCoroutine(AccelerateForTimeAfterDelay(movementBonusSettings.straightSpeedMultiplier,
                    movementBonusSettings.movementBonusType));
                break;
        }
        characterMovementController.isMovementBonus = true;
    }
    
    private IEnumerator AccelerateForTimeAfterDelay(float speedMultiplier, CharacterMovementController.MovementBonusSettings.MovementBonusType movementBonusType)
    {
        yield return new WaitForSeconds(movementBonusSettings.delayBeforeActivation);
        if(movementBonusSettings.movementBonusType != movementBonusType)
            yield break;
        while (movementBonusSettings.effectiveTime > 0)
        {
            characterMovementController.speedBonusMultiply = movementBonusSettings.straightSpeedMultiplier;
            movementBonusSettings.effectiveTime -= Time.deltaTime;
            yield return null;
        }
    }
}
