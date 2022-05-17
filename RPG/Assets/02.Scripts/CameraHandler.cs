using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float minDistance = 3; // ī�޶�� Ÿ�ٰ� �ּҰŸ�
    [SerializeField] private float maxDistance = 30; // ī�޶�� Ÿ�ٰ� �ִ�Ÿ�
    [SerializeField] private float wheelSpeed = 500; // ���콺�� �ӵ�
    [SerializeField] private float xPointSpeed = 500; // ���콺 x �̵��ӵ�
    [SerializeField] private float yPointSpeed = 500; // ���콺 y �̵��ӵ�
    private float yMinLimit = 5;
    private float yMaxLimit = 80;
    private float x, y; // ���콺 ��ġ ( ȸ�� )
    private float distance; // ī�޶�� Ÿ�ٰ��Ÿ�
    private Transform tr;
    private void Awake()
    {
        StartCoroutine(E_Init());
    }

    IEnumerator E_Init()
    {
        yield return new WaitUntil(() => Player.isReady);

        target = Player.instance.transform;

        tr = GetComponent<Transform>();
        distance = Vector3.Distance(tr.position, target.position);
        x = tr.eulerAngles.y;
        y = -tr.eulerAngles.x;
    }

    private void Update()
    {
        if (target == null) return;

        // ���콺 �¿��Է�
        x += Input.GetAxis("Mouse X") * xPointSpeed * Time.deltaTime;
        // ���콺 �����Է�
        y -= Input.GetAxis("Mouse Y") * yPointSpeed * Time.deltaTime;
        ClampAngle(y, -yMinLimit, -yMaxLimit);

        // ���콺 �¿��Է����� y �� ȸ��, 
        // ���콺 �����Է����� x �� ȸ��
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
