using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineManager : MonoBehaviour
{
    public PlayerState playerState;

    [SerializeField] private Transform cam;
    private Transform tr;
    private PlayerMove playerMove;
    private PlayerAnimator playerAnimator;
    private CharacterController characterController;

    private PlayerStateMachine[] machines;
    private PlayerStateMachine currentMachine;


    private void Awake()
    {
        tr = GetComponent<Transform>();
        playerMove = GetComponent<PlayerMove>();
        playerAnimator = GetComponent<PlayerAnimator>();
        characterController = GetComponent<CharacterController>();
        machines = GetComponents<PlayerStateMachine>();
        currentMachine = machines[0];        
    }


    private void Update()
    {
        // movement
        if (playerState == PlayerState.Move ||
            playerState == PlayerState.Jump)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            playerAnimator.SetFloat("h", h);
            playerAnimator.SetFloat("v", v);


            Vector3 move = cam.rotation * new Vector3(h, 0, v);
            playerMove.SetMove(move.x, move.z);
        }
        else
        {
            playerMove.SetMove(0, 0);
        }
        tr.rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);

        // Jump
        if (Input.GetKey(KeyCode.Space))
            ChangePlayerState(PlayerState.Jump);

        // Attack
        if (Input.GetMouseButton(0) &&
            Cursor.visible == false)
            ChangePlayerState(PlayerState.Attack);            

        UpdatePlayerState();
    }

    private void UpdatePlayerState()
    {
        if (currentMachine != null)
            ChangePlayerState(currentMachine.Workflow()); 
    }

    public void ChangePlayerState(PlayerState newState)
    {
        if (playerState == newState) return;

        // �ٲٷ��� �ӽ� �˻�
        foreach (var sub in machines)
        {
            if (sub.playerState == newState &&
                sub.IsExecuteOK()) // �����Ϸ��� �ӽ� ���డ���ϸ�
            {
                currentMachine.ForceStop(); // ���� ���ư��� �ӽ� �ߴ�
                currentMachine = sub; // ���� �ӽ� ����
                currentMachine.Execute(); // ���� �ӽ� ����
                playerState = newState; // ���� ����
                return;
            }
        }
    }

}

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Attack,
}