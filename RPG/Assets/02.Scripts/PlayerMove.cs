using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float gravity = 9.81f;
    private Vector3 _move;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (characterController.isGrounded == false)
            _move += Vector3.down * gravity;

        characterController.Move(_move * Time.deltaTime);
    }

    public void SetMove(float x, float z)
    {
        _move.x = x * moveSpeed;
        _move.z = z * moveSpeed;
    }

    public void SetMove(float y)
    {
        _move.y = y;
    }

    public void SetMove(Vector3 move)
    {
        _move = new Vector3(move.x * moveSpeed,
                            move.y,
                            move.z * moveSpeed);
    }
}