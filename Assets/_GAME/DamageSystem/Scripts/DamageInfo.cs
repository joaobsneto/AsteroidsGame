using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public int DamageValue;
    public ContactPoint2D[] Contacts;

    public DamageInfo(ContactPoint2D[] contacts, int damageValue)
    {
        Contacts = contacts;
        DamageValue = damageValue;
    }
}
