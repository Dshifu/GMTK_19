using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class CharacterMovementController : MonoBehaviour
{
    public int characterVerticalSpeedMultiplier = 500;
    public int characterRotationSpeedMultiplier = 1500;
    public Rigidbody2D rigidbodyComponent;

    private Animator characterAnimator;

    public float speedBonusMultiply = 1f;
    public bool isMovementBonus = false;

    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var move = Input.GetAxis("Vertical");
        var rotate = Input.GetAxis("Horizontal");

        var rotateRaw = Input.GetAxisRaw("Horizontal");
        var moveRaw = Input.GetAxisRaw("Vertical");

        if (rotateRaw < -0.1f)
        {
            characterAnimator.SetBool(PrefsName.AnimatorState.MoveLeft, false);
            characterAnimator.SetBool(PrefsName.AnimatorState.MoveRight, true);
        }
        else if (rotateRaw > 0.1f)
        {
            characterAnimator.SetBool(PrefsName.AnimatorState.MoveRight, false);
            characterAnimator.SetBool(PrefsName.AnimatorState.MoveLeft, true);
        }
        else
        {
            characterAnimator.SetBool(PrefsName.AnimatorState.MoveRight, false);
            characterAnimator.SetBool(PrefsName.AnimatorState.MoveLeft, false);
        }

        if (moveRaw > 0.1f || moveRaw < -0.1f)
            characterAnimator.SetBool(PrefsName.AnimatorState.Move, true);
        else
            characterAnimator.SetBool(PrefsName.AnimatorState.Move, false);


        rigidbodyComponent.AddTorque(rotate * characterRotationSpeedMultiplier * -1f);

        if (!isMovementBonus)
        {
            rigidbodyComponent.AddForce(characterVerticalSpeedMultiplier * move * (Vector2)transform.up);

            return;
        }
        if(move < 0)
            rigidbodyComponent.AddForce(characterVerticalSpeedMultiplier * move * (Vector2)transform.up);
        
        rigidbodyComponent.AddForce(characterVerticalSpeedMultiplier * speedBonusMultiply * (Vector2)transform.up);
    }

    [System.Serializable]
    public class MovementBonusSettings
    {
        public float straightSpeedMultiplier = 1.0f;
        [ShowIf("IsNotShampoo")]
        public float delayBeforeActivation = 0f;
        public float effectiveTime = 1f;
        public MovementBonusType movementBonusType;

        public enum MovementBonusType
        {
            AK,
            Beans,
            Coka,
            FireShit,
            Shampoo
        }

        private bool IsNotShampoo()
        {
            return movementBonusType != MovementBonusType.Shampoo;
        }
    }
}
