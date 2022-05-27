/// <summary>
/// 플레이어의 이름과 스텟 데이터
/// </summary>
public class PlayerData // 단지 데이터 처리용 클래스이기 떄문에 Monobehavior 상속받을 필요 없다. 
{
    public string nickName;
    public Stats stats;
    
    /// <summary>
    /// 스텟 초기화 
    /// todo -> 나중에 스텟을 위한 테이블을 참조하는 형태로 수정해야함.
    /// </summary>
    /// <param name="newNickName"> 생성할 캐릭터 이름 </param>
    public PlayerData(string newNickName) // Monobehavior 를 상속받지 않았기 때문에 생성자 사용 가능.
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

[System.Serializable] // json 포맷 등으로 직렬화/역직렬화 할 수 있게 하기위한 속성
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

    // 연산자 오버로딩
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