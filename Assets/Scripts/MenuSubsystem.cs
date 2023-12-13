using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSubsystem : MonoBehaviour
{
    [SerializeField]
    List<MenuData> menus = new List<MenuData>();

    public void OpenMenu(MenuTag menuTag)
    {
        foreach (MenuData menuData in menus)
        {
            bool isActiveMenu = menuData.MenuTag == menuTag;
			menuData.Menu.gameObject.SetActive(isActiveMenu);
			if (isActiveMenu)
            {
                menuData.Menu.Setup(this);
            }
        }
    }
}

[System.Serializable]
public struct MenuData
{
    [SerializeField]
    MenuTag menuTag; 
    public MenuTag MenuTag => menuTag;

    [SerializeField]
    Menu menu;
    public Menu Menu => menu;
}
