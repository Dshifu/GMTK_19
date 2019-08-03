using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public Rigidbody2D rigidbodyComponent;
    public int characterVerticalSpeedMultiplier = 5;
    public int characterRotationSpeedMultiplier = 100;

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var move = Input.GetAxis("Vertical");
        var rotate = Input.GetAxis("Horizontal");

        rigidbodyComponent.AddTorque(rotate * characterRotationSpeedMultiplier * -1f);
        rigidbodyComponent.AddForce((Vector2)transform.up * characterVerticalSpeedMultiplier * move);
    }
}
