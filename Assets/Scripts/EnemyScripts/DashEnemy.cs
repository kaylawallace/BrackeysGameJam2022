using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    [SerializeField] private int dashDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player plr = collision.GetComponent<Player>();

        if (plr)
        {
            plr.TakeDamage(dashDamage);
        }
    }
}
