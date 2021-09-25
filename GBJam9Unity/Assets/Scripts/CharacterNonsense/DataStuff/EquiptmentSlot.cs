
public class EquiptmentSlot
{
    public eItemType RequiredType = eItemType.none;
    public ItemDetails Equiptment;
    public int Count = 0;

    public ItemDetails AddItem(ItemDetails item)
    {
        ItemDetails outItem = null;
        if(Equiptment == item)
        {
            if (RequiredType != eItemType.none)
            {
                outItem = Equiptment;
            }
            else
            {
                Count++;
            }
        }
        else
        {
            outItem = Equiptment;
            Equiptment = item;
        }
        return outItem;
    }

    public ItemDetails RemoveItem()
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
