public class Potion : Item, IInteractable
{
    public override void Use()
    {
        
        
        Debug.Log("Potion used");
    }

    public void Interact(Inventory inven)
    {
        inven.TryAdd(this);
    }
}