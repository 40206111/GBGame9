[System.Serializable]
public class EquiptmentSlot
{
    public eItemType RequiredType = eItemType.none;
    public ItemDetails Equiptment;
    public int Count = 0;

    public EquiptmentSlot AddItem(ItemDetails item)
    {
        EquiptmentSlot outItem = null;
        if(Equiptment == item)
        {
            if (RequiredType != eItemType.none)
            {
                outItem.Equiptment = Equiptment;
                outItem.Count = Count;
            }
            else
            {
                Count++;
            }
        }
        else
        {
            outItem.Equiptment = Equiptment;
            outItem.Count = Count;
            Equiptment = item;
            Count = 1;
        }
        return outItem;
    }

    public void AddToCount(int value)
    {
        Count += value;
    }

    public ItemDetails SubtractItem()
    {
        ItemDetails outItem = Equiptment;
        Count--;
        if(Count == 0)
        {
            Equiptment = null;
        }
        return outItem;
    }

    public bool HasEquiptment
    {
        get
        {
            return Equiptment != null;
        }
    }
}
