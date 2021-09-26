using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyControl : MonoBehaviour
{
    private static PartyControl _instance;
    public static PartyControl Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private GameObject MenuContainer;
    [SerializeField]
    private EquippedMenuManager EquipMan;
    [SerializeField]
    private PartyMenuHeader Header;

    private bool MenuOpen = false;

    private void Start()
    {
        MenuContainer.SetActive(false);
    }

    public void OpenPartyScreen()
    {
        MenuOpen = true;
        MenuContainer.SetActive(true);
        GameManager.Instance.AddInputTarget(gameObject.GetInstanceID());
        DialogueBoxControl.Instance.Display(true);
        PopulateFromChicken(eChickenClass.melee);
    }

    private List<ItemDetails> GetItemDetails(eChickenClass chickClass)
    {
        EquiptmentLayout layout = PlayerData.GetChickenData(chickClass).EquippedItems;
        List<ItemDetails> ids = new List<ItemDetails>
        {
            GetItemDetail(layout, eItemType.headPiece),
            GetItemDetail(layout, eItemType.chestPiece),
            GetItemDetail(layout, eItemType.legWear),
            GetItemDetail(layout, eItemType.footWear),
            GetItemDetail(layout, eItemType.weapon),
            GetItemDetail(layout, eItemType.trinket, 0),
            GetItemDetail(layout, eItemType.trinket, 1),
            GetItemDetail(layout, eItemType.trinket, 2)
        };

        return ids;
    }

    private ItemDetails GetItemDetail(EquiptmentLayout layout, eItemType itemType, int trinket = 0)
    {
        return layout.GetEquiptmentSlot(itemType, trinket).Equiptment;
    }

    public void HidePartyScreen()
    {
        MenuOpen = false;
        EquipMan.CleanUp();
        MenuContainer.SetActive(false);
        GameManager.Instance.RemoveInputTarget(gameObject.GetInstanceID());
        DialogueBoxControl.Instance.Display(false);
    }

    private void Update()
    {
        if (MenuOpen && GameManager.Instance.IsActiveInputTarget(gameObject.GetInstanceID()))
        {
            if (Input.GetButtonDown("BButton"))
            {
                HidePartyScreen();
            }
            else if (Input.GetButtonDown("Select"))
            {
                ChickenSelectControl.Instance.RunChickenSelect(PopulateFromChicken);
            }
        }
    }

    private void PopulateFromChicken(eChickenClass chickClass)
    {
        EquipMan.PopulateMenu(GetItemDetails(chickClass));
        Header.ChangeChicken(chickClass);
    }
}
