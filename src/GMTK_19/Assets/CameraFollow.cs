using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    [SerializeField] private float cameraMoveSpeed = 1f;


    private void FixedUpdate()
    {
        var cameraFollowPosition = followTarget.position;
        var position = transform.position;
        cameraFollowPosition.z = position.z;

        var cameraMoveDir = (cameraFollowPosition - position).normalized;
        var distance = Vector3.Distance(cameraFollowPosition, position);

        if (!(distance > 0)) return;
        
        var newCameraPosition = transform.position + distance * cameraMoveSpeed * Time.deltaTime * cameraMoveDir;

        var distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);
        if (distanceAfterMoving > distance)
        {
            newCameraPosition = transform.position;
        }

        transform.position = newCameraPosition;
    }
}
