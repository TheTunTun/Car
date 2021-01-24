using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSubMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject subMenu;
    [SerializeField] private GameObject upperMenu;

    public void OpenSubMenus()
    {
        subMenu.SetActive(true);
        upperMenu.SetActive(false);
        
    }

    public void CloseSubMenu()
    {
        subMenu.SetActive(false);
        upperMenu.SetActive(true);
    }
}
