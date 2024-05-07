using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float time = 0;
    float maxTime = 1f;
 
    public void OnEnter(Enemy enemy)
    {
        Vector3 pos = enemy.GetPosition(enemy.centrePoint.position, enemy.attackRange);
        enemy.SetDestination(pos);
    }

    public void OnExecute(Enemy enemy)
    {
        if (enemy.Targets.Count <= 0)
        {
            Vector3 pos = enemy.GetPosition(enemy.centrePoint.position, enemy.attackRange);
            enemy.SetDestination(pos);
        }
        
        if (enemy.Targets.Count > 0)
        {
            enemy.OnStopMove();

            time += Time.deltaTime;
            enemy.OnAttack();
            enemy.ChangeAnim(AnimName.ATTACK);

            if (time >= maxTime)
            {
                time = 0;
                enemy.Throw();
            }
        } else if (enemy.IsDestionation())
        {
            time = 0;
            enemy.ChangeState(new IdleState());
        }
    }
    public void OnExit(Enemy enemy)
    {
        
    }
}
