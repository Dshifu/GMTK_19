using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
<<<<<<< HEAD
    public Rigidbody2D rigidbody2D;
    public int characterVerticalSpeedMultiplier = 500;
    public int characterRotationSpeedMultiplier = 1500;
=======
    public Rigidbody2D rigidbodyComponent;
    public int characterVerticalSpeedMultiplier = 5;
    public int characterRotationSpeedMultiplier = 100;
>>>>>>> 5aad7ecf7e8ee1f1d621687e353006725510cd3d

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
