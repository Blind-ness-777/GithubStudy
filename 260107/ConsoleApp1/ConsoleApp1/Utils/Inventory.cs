public class Inventory
{
    private const int SlotCount = 6;
    private Item[] itemSlots = new Item[SlotCount];
    public bool IsActive { get; set; }
    public MenuList _itemMenu = new MenuList();
    
    public bool TryAdd(Item item)
    {
        int emptySlotIndex = FindEmptySlotIndex();
        if (emptySlotIndex == -1) return false;
        
        itemSlots[emptySlotIndex] = item;
        return true;
    }

    public void Remove(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= SlotCount) return;
        itemSlots[slotIndex] = null;
    }

    public void Use(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= SlotCount) return;
        if (itemSlots[slotIndex] == null) return;

        itemSlots[slotIndex].Use();
        itemSlots[slotIndex] = null;
    }
    
    public Item GetItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= SlotCount) return null;
        return itemSlots[slotIndex];
    }
    
    public bool IsFull()
    {
        return FindEmptySlotIndex() == -1;
    }

    private int FindEmptySlotIndex()
    {
        for (int slotIndex = 0; slotIndex < SlotCount; slotIndex++)
        {
            if (itemSlots[slotIndex] == null)
                return slotIndex;
        }

        return -1;
    }
    
    public void Render()
    {
        if (!IsActive) return;
        
        _itemMenu.Render(15, 1);
    }

    public void Select()
    {
        if (!IsActive) return;
        _itemMenu.Select();
    }

    public void SelectLeft()
    {
        if (!IsActive) return;
        _itemMenu.SelectLeft();
    }

    public void SelectRight()
    {
        if (!IsActive) return;
        _itemMenu.SelectRight();
    }

    public void Remove()
    {
        
    }
}