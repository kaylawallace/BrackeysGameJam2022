using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RemoveHelmetAbility : MonoBehaviour
{
    [HideInInspector] public bool removed;
    public GameObject dungeon, room;
    public Animator transitionAnimator;
    bool cooling;
    GameObject[] rangedEnemies;
    GameObject[] projectiles; 

    // Start is called before the first frame update
    void Start()
    {
        rangedEnemies = GameObject.FindGameObjectsWithTag("RangeEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (removed)
        {
            RemoveHelmet();
        }
    }

    public void RemoveHelmet()
    {
        // play MC helmet anim 
        // play slide anim 
        // set dungeon to inactive and room to active 

        print("taking helmet off");
        DisableEnemies(false);
        DestroyProjectiles();
        transitionAnimator.SetBool("helmetOff", true);
        StartCoroutine(SwitchRooms(true));
        StartCoroutine(SetFalse("helmetOff"));
        StartCoroutine(Cooldown(5f));
    }

    public void PutHelmetBackOn()
    {       
        print("putting helmet back on");
        transitionAnimator.SetBool("helmetOn", true);
        StartCoroutine(SwitchRooms(false));
        StartCoroutine(SetFalse("helmetOn"));
        DisableEnemies(true);
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
            room.SetActive(true);
        }
        else if (!isDungeon)
        {
            dungeon.SetActive(true);
            room.SetActive(false);
        }
    }

    IEnumerator Cooldown(float cooldownTime)
    {
        print("cooling down");
        cooling = true;
        yield return new WaitForSeconds(cooldownTime);
        cooling = false;
        print("done cooling");
        PutHelmetBackOn();
    }

    public void DisableEnemies(bool canShoot)
    {
        for (int i = 0; i < rangedEnemies.Length; i++)
        {
            rangedEnemies[i].GetComponent<EnemyAI>().canShoot = canShoot;
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
