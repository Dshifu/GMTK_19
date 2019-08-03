using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(CharacterMovementController))]
public class MovementBonusEffectController : MonoBehaviour
{
    private CharacterMovementController characterMovementController;
    private Animator characterAnimator;
    
    private CharacterMovementController.MovementBonusSettings movementBonusSettings = null;

    private void Awake()
    {
        characterMovementController = GetComponent<CharacterMovementController>();
        characterAnimator = GetComponent<Animator>();
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
            characterAnimator.SetBool(PrefsName.AnimatorState.IsNeedToPlayParticle, false);
            characterAnimator.SetBool(PrefsName.AnimatorState.Coka, false);
            characterAnimator.SetBool(PrefsName.AnimatorState.FireShit, false);
            characterAnimator.SetBool(PrefsName.AnimatorState.AK, false);
            characterAnimator.SetBool(PrefsName.AnimatorState.Goroh, false);
            characterAnimator.SetBool(PrefsName.AnimatorState.Shampoo, false);
            return;
        }

        if (Input.GetAxisRaw("Vertical") < 1f)
        {
            return;
        }

        switch (movementBonusSettings.movementBonusType)
        {
            case CharacterMovementController.MovementBonusSettings.MovementBonusType.AK:
            case CharacterMovementController.MovementBonusSettings.MovementBonusType.Goroh:
            case CharacterMovementController.MovementBonusSettings.MovementBonusType.FireShit:
                characterMovementController.speedBonusMultiply = movementBonusSettings.straightSpeedMultiplier;
                movementBonusSettings.effectiveTime -= Time.deltaTime;
                characterAnimator.SetBool(movementBonusSettings.movementBonusType.ToString(), true);
                break;
            case CharacterMovementController.MovementBonusSettings.MovementBonusType.Shampoo:
            case CharacterMovementController.MovementBonusSettings.MovementBonusType.Coka:
                
                StartCoroutine(AccelerateForTimeAfterDelay(movementBonusSettings.straightSpeedMultiplier,
                    movementBonusSettings.movementBonusType));
                characterAnimator.SetBool(movementBonusSettings.movementBonusType.ToString(), true);
                break;
        }
        characterMovementController.isMovementBonus = true;
        characterAnimator.SetBool(PrefsName.AnimatorState.IsNeedToPlayParticle, true);
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
