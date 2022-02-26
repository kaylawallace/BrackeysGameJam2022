using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDetection : MonoBehaviour
{
    PlayerCombat plr;

    private void Start()
    {
        plr = GetComponentInParent<PlayerCombat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RangeEnemy") || collision.CompareTag("MeleeEnemy"))
        {
            print("hit enemy");
            collision.GetComponent<Enemy>().TakeDamage(plr.currDamage);
        }
    }
}
