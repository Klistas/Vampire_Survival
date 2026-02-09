using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float CameraSpeed; // 부드럽게 따라올수있도록 제어할 속도
    public Vector3 CameraOffset; // 카메라와 플레이어(대상)간의 거리 제어
    public Transform Target; // 카메라가 따라갈 타겟

    private void LateUpdate()
    {
        if (Target == null) // 타겟이 없으면 진행되지 않도록하여 미리 에러를 차단함.
            return;

        // 이동할 위치에 타겟과 카메라 간의 위치를 계산. 
        Vector3 targetPosition = Target.position + CameraOffset;

        // 그 위치까지 부드럽게 이동할 수 있게 함. 보간
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, CameraSpeed);

        // 카메라에 해당 위치들을 적용.
        transform.position = smoothPosition;
    }

}
