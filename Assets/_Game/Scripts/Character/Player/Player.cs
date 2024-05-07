using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public DynamicJoystick joystick;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    private CounterTime counter = new CounterTime();
    private float throwDelay = 1f;


    public override void OnInit(int weaponTypeIndex, int hatTypeIndex, int pantTypeIndex)
    {
        base.OnInit(weaponTypeIndex, hatTypeIndex, pantTypeIndex);
        agent.enabled = true;
        SetSize(minSize);
        LoadCurrentWeapon((WeaponType)weaponTypeIndex);
        LoadCurrentHat((HatType)hatTypeIndex);
        LoadCurrentPant((PantType)pantTypeIndex);
    }

    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            isGround = CheckGround();

            Vector3 inputJoyStick = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

            if (inputJoyStick.magnitude > 0f)
            {
                ChangeAnim(AnimName.RUN);
                Move(inputJoyStick);
                isMoving = true;
                counter.Cancel();
            }
            else if (inputJoyStick.magnitude <= 0f && Targets.Count <= 0)
            {
                ChangeAnim(AnimName.IDLE);
                isMoving = false;
            }

            if (inputJoyStick.magnitude <= 0f && Targets.Count > 0)
            {
                OnAttack();
                ChangeAnim(AnimName.ATTACK);
                counter.Start(Throw, throwDelay);
                counter.Execute();
            }
        } 
    }

    private void Move(Vector3 direct)
    {
        agent.enabled = true;
        Vector3 targetPos = Camera.main.transform.TransformDirection(direct);
        targetPos.y = 0f;

        agent.Move(moveSpeed * Time.deltaTime * targetPos);

        if (targetPos != Vector3.zero)
        {
            Quaternion targetRos = Quaternion.LookRotation(targetPos);
            Tf.rotation = Quaternion.Slerp(Tf.rotation, targetRos, rotationSpeed * Time.deltaTime);
        }
    }

}
