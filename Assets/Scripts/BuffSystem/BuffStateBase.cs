using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffStateBase {

    public Transform CurrCtrl { get; set; }

    //1.当前Buff的类型（Buff，DeBuff，PerBuff，PerDeBuff）
    public object CurrentBuffType { get; set; }

    //2.具体的Buff状态值：BuffSpeedUp？ BuffAttackUp？。。。
    public object CurrentBuffState { get; set; }
    //3.遵循的参数
    public BuffArgs CurrBuffArgs { get; set; }

    //4.Buff的描述（鼠标移进去显示的内容）
    public abstract string CurrBuffInfo { get; }
    public abstract void OnEnter();
    public abstract void OnUpDate();
    public abstract void OnExit();
}
