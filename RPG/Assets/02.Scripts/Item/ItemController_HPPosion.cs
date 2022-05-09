public class ItemController_HPPosion : ItemController_Spend
{
    public override void Use()
    {
        base.Use();
        Player.instance.hp += 100;
    }
}