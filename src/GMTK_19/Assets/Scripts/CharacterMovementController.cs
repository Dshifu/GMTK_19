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
    private MovementBonusSettings movementBonusSettings = new MovementBonusSettings();

    private bool isMoveBonusActive;

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var move = Input.GetAxis("Vertical");
        var rotate = Input.GetAxis("Horizontal");

        rigidbodyComponent.AddTorque(rotate * characterRotationSpeedMultiplier * -1f);

        if (!isMoveBonusActive)
        {
            rigidbodyComponent.AddForce((Vector2)transform.up * characterVerticalSpeedMultiplier * move);
            return;
        }

        if(Input.GetAxisRaw("Vertical") < 1f)
            return;

        switch (movementBonusSettings.movementBonusType)
        {
            case MovementBonusSettings.MovementBonusType.AK:
            case MovementBonusSettings.MovementBonusType.BEANS:
            case MovementBonusSettings.MovementBonusType.FIRESHIT:
                rigidbodyComponent.AddForce((Vector2)transform.up * characterVerticalSpeedMultiplier * movementBonusSettings.straightSpeedMultiplier);
                movementBonusSettings.effectiveTime -= Time.deltaTime;
                break;
            case MovementBonusSettings.MovementBonusType.SHAMPOO:
            case MovementBonusSettings.MovementBonusType.COKE:
                StartCoroutine(AccelerateForTimeAfterDelay((Vector2)transform.up * characterVerticalSpeedMultiplier * movementBonusSettings.straightSpeedMultiplier,
                                                                movementBonusSettings.movementBonusType));
                break;
        }

        if (movementBonusSettings.effectiveTime < 0)
            isMoveBonusActive = false;

    }

    private IEnumerator AccelerateForTimeAfterDelay(Vector2 force, MovementBonusSettings.MovementBonusType movementBonusType)
    {
        yield return new WaitForSeconds(movementBonusSettings.delayBeforeActivation);
        if(movementBonusSettings.movementBonusType != movementBonusType)
            yield break;
        while (movementBonusSettings.effectiveTime > 0)
        {
            rigidbodyComponent.AddForce(force);
            movementBonusSettings.effectiveTime -= Time.deltaTime;
            yield return null;
        }
    }

    public void SetMovementBonus(MovementBonusSettings collectedMovementBonusSettings)
    {
        movementBonusSettings = collectedMovementBonusSettings;
        isMoveBonusActive = true;
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
