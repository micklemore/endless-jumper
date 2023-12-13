using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private MenuSubsystem menuSubsystem;

    public void Setup(MenuSubsystem menuSubsystem)
    {
        this.menuSubsystem = menuSubsystem;
    }
}
