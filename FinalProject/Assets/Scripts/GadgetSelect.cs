using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GadgetSelect : MonoBehaviour
{
    public Gadgets gadgets;
    [SerializeField]
    private GameObject gadgetSelectionMenu;
    // Start is called before the first frame update
    public void GadgetClicked()
    {
        string GadgetSelected = EventSystem.current.currentSelectedGameObject.name;
        switch (GadgetSelected)
        {
            case "Teleport":
                gadgets.gadgetType = "Teleport";
                break;
            case "Invisibility":
                gadgets.gadgetType = "Invisibility";
                break;
            case "Rage":
                gadgets.gadgetType = "Rage";
                break;
        }
        CloseGadgetSelect();
    }
    public void CloseGadgetSelect()
    {
        gadgetSelectionMenu.SetActive(false);
    }
}
