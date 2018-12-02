public class Soldier : NPC {
    protected override void Interact()
    {
        GameController.instance.SacrificeTheLoot();
    }
}
