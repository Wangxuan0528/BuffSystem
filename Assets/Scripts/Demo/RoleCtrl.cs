using UnityEngine;

public class RoleCtrl : MonoBehaviour {
    BuffMaganer buffMaganer;
    private void Start()
    {
        buffMaganer = new BuffMaganer(transform);
    }
    void Update () {

        buffMaganer.OnUpdate();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            BuffArgs buffArgs = new BuffArgs();
            buffArgs.m_cintinueTime = 5;
            buffArgs.m_addition = false;
            buffArgs.m_fValue1 = 10;
            buffMaganer.BuffAdd(BuffType.Buff.BuffSpeedUp, buffArgs);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            BuffArgs buffArgs = new BuffArgs();
            buffArgs.m_timer = 1;
            buffArgs.m_times = 5;
            buffArgs.m_addition = false;
            buffArgs.m_fValue1 = 10;
            buffMaganer.PerBuffAdd(BuffType.PerBuff.BufferAddBlood, buffArgs);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buffMaganer.BuffRemove(BuffType.Buff.BuffSpeedUp);
        }
    }
}
