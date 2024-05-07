using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float time = 0;
    float maxTime = 1;

    public void OnEnter(Enemy enemy)
    {
        enemy.OnStopMove();
        enemy.ChangeAnim(AnimName.IDLE);
    }

    public void OnExecute(Enemy enemy)
    {
        time += Time.deltaTime;
        if (time > maxTime)
        {
            time = 0;
            enemy.ChangeState(new PatrolState());
        }

    }
    public void OnExit(Enemy enemy)
    {

    }

}
