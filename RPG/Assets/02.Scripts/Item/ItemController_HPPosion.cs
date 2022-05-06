public class ItemController_HPPosion : ItemController
{
    public override void Use()
    {
        base.Use();
        Player.instance.hp += 100;
    }
}