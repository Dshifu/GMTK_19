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

    public float speedBonusMultiply = 1f;
    public bool isMovementBonus = false;

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var move = Input.GetAxis("Vertical");
        var rotate = Input.GetAxis("Horizontal");

        rigidbodyComponent.AddTorque(rotate * characterRotationSpeedMultiplier * -1f);

        if (!isMovementBonus)
        {
            rigidbodyComponent.AddForce(characterVerticalSpeedMultiplier * move * (Vector2)transform.up);
            return;
        }
        
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
            BEANS,
            COKE,
            FIRESHIT,
            SHAMPOO
        }

        private bool IsNotShampoo()
        {
            return movementBonusType != MovementBonusType.SHAMPOO;
        }
    }
}
