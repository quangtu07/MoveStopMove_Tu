using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    //public float range = 9f; //radius of sphere
    public Transform centrePoint; //centre of the area the agent wants to move around in

    [SerializeField] private CharacterWeapon playerWeapon;
    private IState currentState;
    private int minIndexItem = 0;
    private int maxIndexItem = 2;

    public override void OnInit(int weaponTypeIndex = 0, int hatTypeIndex = 0, int pantTypeIndex = 0)
    {
        base.OnInit(weaponTypeIndex, hatTypeIndex, pantTypeIndex);
        agent.enabled = true;
        agent.speed = 10f;
        currentWeaponType = (WeaponType) Random.Range(minIndexItem, maxIndexItem);
        currentHatType = (HatType) Random.Range(minIndexItem, maxIndexItem);
        currentPantType = (PantType) Random.Range(minIndexItem, maxIndexItem);
        ChangeState(new IdleState());
        LoadCurrentWeapon(currentWeaponType);
        LoadCurrentHat(currentHatType);
        LoadCurrentPant(currentPantType);
    }

    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            isGround = CheckGround();

            if (currentState != null && !IsDead())
            {
                currentState.OnExecute(this);
            }
        }
    }

    public bool IsDestionation()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    public void SetDestination(Vector3 destination)
    {
        agent.enabled = true;
        if (destination != Vector3.zero) //pass in our centre point and radius of area
        {
            ChangeAnim(AnimName.RUN);
            agent.SetDestination(destination);
        }
    }

    public Vector3 GetPosition(Vector3 center, float range)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        Vector3 result;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return result;
        }

        result = Vector3.zero;
        return result;
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagName.TAG_WALL)) {
            Tf.position = Vector3.zero;
        }
    }

    public override void OnStopMove()
    {
        base.OnStopMove();
        //ChangeAnim(AnimName.IDLE);
        agent.SetDestination(agent.transform.position); // Đặt mục tiêu di chuyển đến vị trí hiện tại
    }
}
