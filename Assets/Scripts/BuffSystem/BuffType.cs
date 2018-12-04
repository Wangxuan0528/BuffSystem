/// <summary>
/// Buff类型，根据项目拓展
/// </summary>
public class BuffType
{
    //Buff类型
    //正面Buff:增加攻速、移速、防御、攻击等
    //负面DeBuff 降低攻速、移速、防御、攻击等

    //周期性正面PerBuff：每4秒回复100血
    //周期性负面PeiDeBuff：每4秒掉100血

    public enum Buff
    {
        AllType,
        BuffSpeedUp,
        BuffInvincible,//无敌
    }

    public enum DeBuff
    {
        AllType,
        BuffSlowDowm,
        BuffDisarm, //缴械
    }

    public enum PerBuff
    {
        AllType,
        BufferAddBlood,
        BuffAddMagic,
    }

    public enum PerDeBuff
    {
        AllType,
        BuffCRBlood,//周期性减血
    }
}
