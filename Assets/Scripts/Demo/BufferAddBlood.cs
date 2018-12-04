using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferAddBlood : BuffStateBase
{
    private float timer ;
    private int times;
    public override string CurrBuffInfo
    {
        get
        {
            return "持续5秒,每秒回复10滴血";
        }
    }

    public override void OnEnter()
    {
        Debug.Log("加血的PerBuff: OnEnter");
        timer = CurrBuffArgs.m_timer;
        times = CurrBuffArgs.m_times;
    }

    public override void OnExit()
    {
        Debug.Log("加血的PerBuff: OnExit");
    }

    public override void OnUpDate()
    {
        if (times > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                AddBlood(CurrBuffArgs.m_fValue1);
                times -= 1;
                timer = CurrBuffArgs.m_timer;
            }
        }
        else
        {
            CurrBuffArgs.isOver = true;
        }

    }

    void AddBlood(float bloodValue)
    {
        Debug.Log("恢复了" + bloodValue + "滴血量");
    }
}
