using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeedUp : BuffStateBase
{
    public override string CurrBuffInfo
    {
        get
        {
           return "这是一个提升10%移速的Buff";
        }
    }

    public override void OnEnter()
    {
        Debug.Log("提速Buff：OnEnter");
    }

    public override void OnExit()
    {
        Debug.Log("提速Buff：OnExit");
    }

    public override void OnUpDate()
    {
        Debug.Log("提速Buff的剩余时间：" + CurrBuffArgs.m_cintinueTime);
        if( (CurrBuffArgs.m_cintinueTime-=Time.deltaTime)<=0)
        {
            CurrBuffArgs.isOver = true;
        }
    }
}
