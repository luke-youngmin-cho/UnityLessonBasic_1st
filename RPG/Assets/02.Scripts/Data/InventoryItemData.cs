/// <summary>
/// 아이템 객체 자체를 저장하면 너무 크기때문에 데이터를 최소화해서 저장하기위해 새로 만든 아이템 데이터관리용 클래스
/// </summary>
[System.Serializable]
public class InventoryItemData
{
    public string key;
    public ItemType type; // (장비, 소비, 기타)
    public string itemName; // 이름
    public int num; // 보유 갯수
    public int slotID; // 해당 아이템이 존재하는 슬롯 번호
}