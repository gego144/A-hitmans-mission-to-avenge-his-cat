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
    [SerializeField] private AudioSource UIClickSE;
    public void GadgetClicked()
    {
        string GadgetSelected = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(GadgetSelected);
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
        UIClickSE.Play();
        CloseGadgetSelect();
    }
    public void CloseGadgetSelect()
    {
        gadgetSelectionMenu.SetActive(false);
    }
}
