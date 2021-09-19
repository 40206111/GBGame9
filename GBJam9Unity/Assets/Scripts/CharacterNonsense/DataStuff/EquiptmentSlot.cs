
public class EquiptmentSlot
{
    public eItemType RequiredType = eItemType.none;
    public ItemDetails Equiptment;

    public bool HasEquiptment
    {
        get
        {
            return Equiptment != null;
        }
    }
}
