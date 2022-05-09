public class ItemController_MPPosion : ItemController_Spend
{
    public override void Use()
    {
        base.Use();
        Player.instance.mp += 100;
    }
}