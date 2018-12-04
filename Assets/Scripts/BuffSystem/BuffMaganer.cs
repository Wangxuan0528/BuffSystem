using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffMaganer
{
    //1.该控制器不需要挂载到游戏对象身上，作为一个脚本的字段存在
    //2.和之前的人物控制不同，这个FSM是or类型，所有状态可以同时运行
    //3.需要一个容器，存放当前人物的所有Buff
    //4.在每一帧中，遍历容器，调用容器中所有状态的UpDate方法
    //5.根据Buff的m_addition,来判断是否更新原来Buff的持续时间
    //如果条件为假：则不能添加新的Buff，但是能够刷新原来Buff的持续时间
    //如果条件为真：则可以添加新的Buff，相同的Buff同时工作

    //6.公开的接口也一样
    //增加一个Buff，移除一个Buff，判断一个Buff是否在容器中存在

    //状态机控制的人物对象
    public Transform CurrCtrl { get; set; }

    public BuffMaganer(Transform trans)
    {
        CurrCtrl = trans;
    }

    public List<BuffStateBase> m_BuffList = new List<BuffStateBase>();

    //公开的方法

    //1.增加一个Buff
    private void BuffAddBase(object buff, BuffArgs buffArgs)
    {
        //反射技术 -->根据字符串名称创建一个对象实例
        BuffStateBase newBuff = Activator.CreateInstance(Type.GetType(buff.ToString())) as BuffStateBase;
        newBuff.CurrBuffArgs = buffArgs;
        newBuff.CurrentBuffType = buff.GetType();
        newBuff.CurrentBuffState = buff;
        newBuff.CurrCtrl = CurrCtrl;

        //将Buff添加到容器中
        //m_addition
        if (buffArgs.m_addition)
        {
            newBuff.OnEnter();
            m_BuffList.Add(newBuff);
        }
        else
        {
            //判断当前容器中，是否包含这个Buff
            BuffStateBase findBuff = BuffCheckExitsBase(buff);
            if (findBuff != null)
            {
                findBuff.CurrBuffArgs.m_cintinueTime = newBuff.CurrBuffArgs.m_cintinueTime;
            }
            //如果没有，则直接加进去
            else
            {
                newBuff.OnEnter();
                m_BuffList.Add(newBuff);
            }
        }
    }
    public void BuffAdd(BuffType.Buff buff, BuffArgs buffArgs)
    {
        BuffAddBase(buff, buffArgs);
    }
    public void DeBuffAdd(BuffType.DeBuff deBuff, BuffArgs buffArgs)
    {
        BuffAddBase(deBuff, buffArgs);
    }
    public void PerBuffAdd(BuffType.PerBuff perBuff, BuffArgs buffArgs)
    {
        BuffAddBase(perBuff, buffArgs);
    }
    public void PerDeBuffAdd(BuffType.PerDeBuff perDeBuff, BuffArgs buffArgs)
    {
        BuffAddBase(perDeBuff, buffArgs);
    }
    //2.移除一个Buff
    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffType">buffType==null:表示删除所有Buff，否则删除某一个Buff</param>
    private void BuffRemoveBase(object buffType = null)
    {
        if (buffType == null)
        {
            m_BuffList.RemoveAll(delegate (BuffStateBase obj)
            {
                obj.OnExit();
                return true;
            });
        }
        //删除某一类Buff（比如被同时加了N个一种类型的Buff）
        if ((int)buffType == 0)
        {
            m_BuffList.RemoveAll(delegate (BuffStateBase obj)
            {
                if (obj.CurrentBuffType.Equals(buffType.GetType()))
                {
                    obj.OnExit();
                    return true;
                }
                return false;
            });
        }
        //删除单个Buff
        else
        {
            m_BuffList.RemoveAll(delegate (BuffStateBase obj)
            {
                if (obj.CurrentBuffType.Equals(buffType.GetType()))
                {
                    obj.OnExit();
                    return true;
                }
                return false;
            });
        }
    }
    public void BuffRemove(BuffType.Buff buffType)
    {
        BuffRemoveBase(buffType);
    }
    public void BuffRemove(BuffType.DeBuff buffType)
    {
        BuffRemoveBase(buffType);
    }
    public void BuffRemove(BuffType.PerBuff buffType)
    {
        BuffRemoveBase(buffType);
    }
    public void BuffRemove(BuffType.PerDeBuff buffType)
    {
        BuffRemoveBase(buffType);
    }
    public void BuffRemove()
    {
        BuffRemoveBase();
    }

    //遍历容器，使每个Buff发生作用
    public void OnUpdate()
    {
        //m_BuffList.Find(delegate (BuffStateBase obj)
        //{
        //    obj.OnUpDate();
        //    return true;
        //});
        foreach (BuffStateBase item in m_BuffList)
        {
            item.OnUpDate();
        }
        m_BuffList.RemoveAll(delegate (BuffStateBase obj)
        {
            if (obj.CurrBuffArgs.isOver)
            {
                obj.OnExit();
                return true;
            }
            return false;
        });
    }

    //检查容器中是否存在Buff
    private BuffStateBase BuffCheckExitsBase(object buff)
    {
        BuffStateBase findBuff = m_BuffList.Find(obj => obj.CurrentBuffState.Equals(buff));
        return findBuff == null ? null : findBuff;
    }
    public BuffStateBase BuffCheckExits(BuffType.Buff buff)
    {
        return BuffCheckExitsBase(buff);
    }
    public BuffStateBase DeBuffCheckExits(BuffType.Buff deBuff)
    {
        return BuffCheckExitsBase(deBuff);
    }
    public BuffStateBase PerBuffCheckExits(BuffType.Buff perBuff)
    {
        return BuffCheckExitsBase(perBuff);
    }
    public BuffStateBase PerDeBuffCheckExits(BuffType.Buff perDeBuff)
    {
        return BuffCheckExitsBase(perDeBuff);
    }

}
