using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterTime
{
    UnityAction doneAction;
    private float time = 0;
    private float limitTime;

    public void Start(UnityAction doneAction, float limitTime)
    {
        this.doneAction = doneAction;
        this.limitTime = limitTime;
    }

    public void Execute()
    {
        if (time < limitTime)
        {
            time += Time.deltaTime;
            if (time >= limitTime)
            {
                Exit();
            }
        }
    }

    public void Exit()
    {
        doneAction?.Invoke();
    }

    public void Cancel()
    {
        doneAction = null;
        time = 0;
    }
}
