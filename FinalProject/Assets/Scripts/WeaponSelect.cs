using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weapons;
    [SerializeField]
    private GameObject weaponSelectionMenu;
    [SerializeField]
    private GameObject gadgetSelectionMenu;

    public void WeaponClicked()
    {
        string weaponSelected = EventSystem.current.currentSelectedGameObject.name;
        switch (weaponSelected)
        {
            case "Dagger":
                weapons[0].SetActive(true);
                break;
            case "Sword":
                weapons[1].SetActive(true);
                break;
            case "Pistol":
                weapons[2].SetActive(true);
                break;
            case "Rifle":
                weapons[3].SetActive(true);
                break;
            case "Sniper":
                weapons[4].SetActive(true);
                break;
            case "Shuriken":
                weapons[5].SetActive(true);
                break;
        }
        CloseWeaponSelect();
        OpenGadgetSelect();
    }
    public void CloseWeaponSelect()
    {
        weaponSelectionMenu.SetActive(false);
    }

    public void OpenGadgetSelect()
    {
        gadgetSelectionMenu.SetActive(true);
    }
}
