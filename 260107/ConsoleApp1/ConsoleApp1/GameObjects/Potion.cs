public class Potion : Item, IInteractable
{
    public override void Use()
    {
        Inventory.Remove(this);
        Inventory = null;
        
        Debug.Log("Potion used");
    }

    public void Interact(Inventory inven)
    {
        inven.TryAdd(this);
    }
}