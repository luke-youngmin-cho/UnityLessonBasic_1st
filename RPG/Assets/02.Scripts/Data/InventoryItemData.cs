/// <summary>
/// ������ ��ü ��ü�� �����ϸ� �ʹ� ũ�⶧���� �����͸� �ּ�ȭ�ؼ� �����ϱ����� ���� ���� ������ �����Ͱ����� Ŭ����
/// </summary>
[System.Serializable]
public class InventoryItemData
{
    public string key;
    public ItemType type; // (���, �Һ�, ��Ÿ)
    public string itemName; // �̸�
    public int num; // ���� ����
    public int slotID; // �ش� �������� �����ϴ� ���� ��ȣ
}