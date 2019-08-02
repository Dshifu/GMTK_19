using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public int characterVerticalSpeedMultiplier = 5;
    public int characterRotationSpeedMultiplier = 100;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var move = Input.GetAxis("Vertical");
        var rotate = Input.GetAxis("Horizontal");

        rigidbody2D.AddTorque(rotate * characterRotationSpeedMultiplier * -1f);
        rigidbody2D.AddForce((Vector2)transform.up * characterVerticalSpeedMultiplier * move);
    }
}
