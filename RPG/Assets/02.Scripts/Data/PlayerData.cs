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
}



//[System.Serializable]
//public struct SavePoint
//{
//    public int mapIdx;
//    public float coordx;
//    public float coordy;
//}