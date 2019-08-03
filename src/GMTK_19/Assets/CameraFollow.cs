using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    [SerializeField] float cameraMoveSpeed;


    void FixedUpdate()
    {
        var cameraFollowPosition = followTarget.position;
        cameraFollowPosition.z = transform.position.z;

        var cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        var distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if (distance > 0)
        {
            var newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            var distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);
            if (distanceAfterMoving > distance)
            {
                newCameraPosition = transform.position;
            }

            transform.position = newCameraPosition;
        }
    }
}
