/// <summary>
/// �÷��̾��� �̸��� ���� ������
/// </summary>
public class PlayerData // ���� ������ ó���� Ŭ�����̱� ������ Monobehavior ��ӹ��� �ʿ� ����. 
{
    public string nickName;
    public Stats stats;
    
    /// <summary>
    /// ���� �ʱ�ȭ 
    /// todo -> ���߿� ������ ���� ���̺��� �����ϴ� ���·� �����ؾ���.
    /// </summary>
    /// <param name="newNickName"> ������ ĳ���� �̸� </param>
    public PlayerData(string newNickName) // Monobehavior �� ��ӹ��� �ʾұ� ������ ������ ��� ����.
    {
        nickName = newNickName;
        stats = new Stats()
        {
            LV = 0,
            EXP = 0,

            STR = 10,
            DEX = 10,
            INT = 10,
            LUK = 10,

            HP = 100,
            HPMax = 100,
            MP = 50,
            MPMax = 50,

            ATK = 10,
            DEF = 1,

            statPoint = 0
        };
        
    }

    
}

[System.Serializable] // json ���� ������ ����ȭ/������ȭ �� �� �ְ� �ϱ����� �Ӽ�
public class Stats
{
    public int LV;
    public int EXP;
    public int STR;
    public int DEX;
    public int INT;
    public int LUK;
    public int HP;    
    public int MP;        
    public int ATK;
    public int DEF;
    public int HPMax;
    public int MPMax;
    public int statPoint;

    // ������ �����ε�
    public static Stats operator +(Stats stats1, Stats stats2)
    {
        stats1.STR += stats2.STR;
        stats1.DEX += stats2.DEX;
        stats1.INT += stats2.INT;
        stats1.LUK += stats2.LUK;
        stats1.ATK += stats2.ATK;
        stats1.DEF += stats2.DEF;
        stats1.HPMax += stats2.HPMax;
        stats1.MPMax += stats2.MPMax;

        return stats1;
    }
    public static Stats operator -(Stats stats1, Stats stats2)
    {
        stats1.STR -= stats2.STR;
        stats1.DEX -= stats2.DEX;
        stats1.INT -= stats2.INT;
        stats1.LUK -= stats2.LUK;
        stats1.ATK -= stats2.ATK;
        stats1.DEF -= stats2.DEF;
        stats1.HPMax -= stats2.HPMax;
        stats1.MPMax -= stats2.MPMax;

        return stats1;
    }
}



//[System.Serializable]
//public struct SavePoint
//{
//    public int mapIdx;
//    public float coordx;
//    public float coordy;
//}