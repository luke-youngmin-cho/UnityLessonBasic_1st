using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float minDistance = 3; // 카메라와 타겟간 최소거리
    [SerializeField] private float maxDistance = 30; // 카메라와 타겟간 최대거리
    [SerializeField] private float wheelSpeed = 500; // 마우스휠 속도
    [SerializeField] private float xPointSpeed = 500; // 마우스 x 이동속도
    [SerializeField] private float yPointSpeed = 500; // 마우스 y 이동속도
    private float yMinLimit = 5;
    private float yMaxLimit = 80;
    private float x, y; // 마우스 위치 ( 회전 )
    private float distance; // 카메라와 타겟간거리
    private Transform tr;
    private void Awake()
    {
        tr = GetComponent<Transform>();
        distance = Vector3.Distance(tr.position, target.position);
        x = tr.eulerAngles.y;
        y = -tr.eulerAngles.x;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (target == null) return;

        // 마우스 좌우입력
        x += Input.GetAxis("Mouse X") * xPointSpeed * Time.deltaTime;
        // 마우스 상하입력
        y -= Input.GetAxis("Mouse Y") * yPointSpeed * Time.deltaTime;
        ClampAngle(y, -yMinLimit, -yMaxLimit);

        // 마우스 좌우입력으로 y 축 회전, 
        // 마우스 상하입력으로 x 축 회전
        tr.rotation = Quaternion.Euler(y, x, 0);

        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private void LateUpdate()
    {
        if(target == null) return;

        tr.position = tr.rotation * new Vector3(0, 0, -distance) + target.position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
