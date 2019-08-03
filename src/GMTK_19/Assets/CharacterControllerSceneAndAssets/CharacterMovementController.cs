using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public int characterVerticalSpeedMultiplier = 500;
    public int characterRotationSpeedMultiplier = 1500;
    public Rigidbody2D rigidbodyComponent;

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
