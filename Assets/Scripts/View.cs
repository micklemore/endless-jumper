using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    protected Menu owner;

    public void Setup(Menu owner)
    {
        this.owner = owner;
    }
}
