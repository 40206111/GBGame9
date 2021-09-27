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
    public List<EquiptmentSlot> TrinketSlots = new List<EquiptmentSlot>() {
        new EquiptmentSlot { RequiredType = eItemType.trinket},
        new EquiptmentSlot { RequiredType = eItemType.trinket},
        new EquiptmentSlot { RequiredType = eItemType.trinket}};

    // For only slots containing items
    public List<EquiptmentSlot> GetAllEquipped()
    {
        List<EquiptmentSlot> outList = new List<EquiptmentSlot>();

        CheckAndAdd(HeadSlot, ref outList);
        CheckAndAdd(BodySlot, ref outList);
        CheckAndAdd(LegSlot, ref outList);
        CheckAndAdd(FootSlot, ref outList);
        CheckAndAdd(WeaponSlot, ref outList);
        foreach (EquiptmentSlot es in TrinketSlots)
        {
            CheckAndAdd(es, ref outList);
        }

        return outList;
    }

    // For every slot
    public List<EquiptmentSlot> GetAllSlots()
    {
        List<EquiptmentSlot> outList = new List<EquiptmentSlot>
        {
            HeadSlot,
            BodySlot,
            LegSlot,
            FootSlot,
            WeaponSlot
        };
        foreach (EquiptmentSlot es in TrinketSlots)
        {
            outList.Add(es);
        }

        return outList;
    }

    public EquiptmentSlot GetEquiptmentSlot(eItemType slot, int trinketSlot = 0)
    {
        return slot switch
        {
            eItemType.headPiece => HeadSlot,
            eItemType.chestPiece => BodySlot,
            eItemType.legWear => LegSlot,
            eItemType.footWear => FootSlot,
            eItemType.weapon => WeaponSlot,
            eItemType.trinket => TrinketSlots[Mathf.Clamp(trinketSlot, 0, TrinketSlots.Count)],
            _ => HeadSlot,
        };
    }

    private void CheckAndAdd(EquiptmentSlot es, ref List<EquiptmentSlot> list)
    {
        if (es.Equiptment != null)
        {
            list.Add(es);
        }
    }

    public ItemDetails EquipItem(ItemDetails item, eItemType slotType, int trinketSlot = 0)
    {
        ItemDetails outItem = null;
        EquiptmentSlot slot = GetEquiptmentSlot(slotType, trinketSlot);
        if (slot.HasEquiptment)
        {
            outItem = (slot.Equiptment);
        }
        slot.Equiptment = item;
        slot.Count = 1;
        return outItem;
    }
}
