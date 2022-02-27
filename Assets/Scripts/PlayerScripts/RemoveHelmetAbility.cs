using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RemoveHelmetAbility : MonoBehaviour
{
    [HideInInspector] public bool removed;
    public GameObject dungeon, room;
    public GameObject dungeonTiles, roomTiles;
    public Animator transitionAnimator;
    bool cooling;
    GameObject[] rangedEnemies, dashEnemies;
    GameObject[] projectiles;
    float abilityCooldownTime = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rangedEnemies = GameObject.FindGameObjectsWithTag("RangeEnemy");
        dashEnemies = GameObject.FindGameObjectsWithTag("MeleeEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (removed && !cooling)
        {
            RemoveHelmet();
        }
    }

    public void RemoveHelmet()
    {
        // play MC helmet anim 
        // play slide anim 
        // set dungeon to inactive and room to active 

        cooling = true;
        DisableEnemies(false);
        DestroyProjectiles();
        transitionAnimator.SetBool("helmetOff", true);
        StartCoroutine(SwitchRooms(true));
        StartCoroutine(SetFalse("helmetOff"));
        StartCoroutine(SwitchBackToDungeon(5f));
    }

    public void PutHelmetBackOn()
    {
        cooling = true;
        transitionAnimator.SetBool("helmetOn", true);
        StartCoroutine(SwitchRooms(false));
        StartCoroutine(SetFalse("helmetOn"));
        DisableEnemies(true);
        StartCoroutine(Cooldown(20f));
    }

    public void OnRemoved(InputAction.CallbackContext context)
    {
        removed = context.action.triggered;
    }

    IEnumerator SetFalse(string boolName)
    {
        yield return new WaitForSeconds(0.5f);
        transitionAnimator.SetBool(boolName, false);
    }

    IEnumerator SwitchRooms(bool isDungeon)
    {
        yield return new WaitForSeconds(0.5f);
        if (isDungeon)
        {
            dungeon.SetActive(false);
            dungeonTiles.SetActive(false);
            room.SetActive(true);
            roomTiles.SetActive(true);
        }
        else if (!isDungeon)
        {
            dungeon.SetActive(true);
            dungeonTiles.SetActive(true);
            room.SetActive(false);
            roomTiles.SetActive(false);
        }
    }

    IEnumerator SwitchBackToDungeon(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        PutHelmetBackOn();
    }

    IEnumerator Cooldown(float cooldownTime)
    {
        cooling = true;
        yield return new WaitForSeconds(cooldownTime);
        cooling = false; 
    }

    public void DisableEnemies(bool canAttack)
    {
        for (int i = 0; i < rangedEnemies.Length; i++)
        {
            rangedEnemies[i].GetComponent<EnemyAI>().canShoot = canAttack;
        }

        for (int i = 0; i < dashEnemies.Length; i++)
        {
            dashEnemies[i].GetComponent<EnemyAI>().canDash = canAttack;
        }
    }

    public void DestroyProjectiles()
    {
        projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        for (int i = 0; i < projectiles.Length; i++)
        {
            Destroy(projectiles[i], 0.5f);
        }
    }
}
