using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptmentLayout
{
    public EquiptmentSlot HeadSlot = new EquiptmentSlot { RequiredType = eItemType.headPiece };
    public EquiptmentSlot BodySlot = new EquiptmentSlot { RequiredType = eItemType.chestPiece };
    public EquiptmentSlot LegSlot = new EquiptmentSlot { RequiredType = eItemType.legWear };
    public EquiptmentSlot FootSlot = new EquiptmentSlot { RequiredType = eItemType.footWear };
    public EquiptmentSlot WeaponSlot = new EquiptmentSlot { RequiredType = eItemType.weapon };
    public List<EquiptmentSlot> TrincketSlots = new List<EquiptmentSlot>() {
        new EquiptmentSlot { RequiredType = eItemType.trinket},
        new EquiptmentSlot { RequiredType = eItemType.trinket},
        new EquiptmentSlot { RequiredType = eItemType.trinket}};

    public List<EquiptmentSlot> GetAllItems()
    {
        List<EquiptmentSlot> outList = new List<EquiptmentSlot>();

        CheckAndAdd(HeadSlot, ref outList);
        CheckAndAdd(BodySlot, ref outList);
        CheckAndAdd(LegSlot, ref outList);
        CheckAndAdd(FootSlot, ref outList);
        CheckAndAdd(WeaponSlot, ref outList);
        foreach (EquiptmentSlot es in TrincketSlots)
        {
            CheckAndAdd(es, ref outList);
        }

        return outList;
    }

    private void CheckAndAdd(EquiptmentSlot es, ref List<EquiptmentSlot> list)
    {
        if (es.Equiptment != null)
        {
            list.Add(es);
        }
    }
}
