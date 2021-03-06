﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int AttackDamage = 1;

    public bool IsActive { get; set; } = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActive) return;
        var followRigidbody = collision.rigidbody.GetComponent<FollowRigidbody>();
        Rigidbody2D target = (followRigidbody != null) ? followRigidbody.Target : collision.rigidbody;
        var damageable = target.GetComponent<Damageable>();
        if (damageable != null) damageable.TakeDamage(new DamageInfo(collision.contacts, AttackDamage));
    }

}
