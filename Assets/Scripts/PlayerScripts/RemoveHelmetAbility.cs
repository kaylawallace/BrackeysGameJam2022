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
    public SpriteRenderer[] dungeonRen, roomRen;
    public Animator playerAnim; 
    private AudioManager am;

    float abilityCooldownTime = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rangedEnemies = GameObject.FindGameObjectsWithTag("RangeEnemy");
        dashEnemies = GameObject.FindGameObjectsWithTag("MeleeEnemy");
        am = FindObjectOfType<AudioManager>();

        for (int i = 0; i < roomRen.Length; i++)
        {
            roomRen[i].enabled = false; 
        }
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

        rangedEnemies = GameObject.FindGameObjectsWithTag("RangeEnemy");
        dashEnemies = GameObject.FindGameObjectsWithTag("MeleeEnemy");

        playerAnim.SetTrigger("RemoveHelmet");
        am.Play("Remove_Helmet");
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
        rangedEnemies = GameObject.FindGameObjectsWithTag("RangeEnemy");
        dashEnemies = GameObject.FindGameObjectsWithTag("MeleeEnemy");

        playerAnim.SetTrigger("DonHelmet");
        am.Play("Don_Helmet");
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
            DisableRenderers(true);
            dungeonTiles.SetActive(false);
            room.SetActive(true);
            roomTiles.SetActive(true);
        }
        else if (!isDungeon)
        {
            dungeon.SetActive(true);
            DisableRenderers(false);
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

    public void DisableRenderers(bool isDungeon)
    {
        if (isDungeon)
        {
            for (int i = 0; i < dungeonRen.Length; i++)
            {
                dungeonRen[i].enabled = false;
            }

            for (int i = 0; i < roomRen.Length; i++)
            {
                roomRen[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < dungeonRen.Length; i++)
            {
                dungeonRen[i].enabled = true;
            }

            for (int i = 0; i < roomRen.Length; i++)
            {
                roomRen[i].enabled = false;
            }
        }
    }
}
