using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    private Animator anim;
    private AudioManager am;
    public HealthBar healthBar;
    public LevelLoader levelLoader;

    private int health;
    private bool justDamaged;
    private float cooldown = 1f;
    GameObject[] rangedEnemies, dashEnemies;

    void Start()
    {  
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponentInChildren<Animator>();
        am = FindObjectOfType<AudioManager>();
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        anim.SetBool("Dead", false);
    }

    private void Update()
    {
        if (justDamaged)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0)
            {
                justDamaged = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!justDamaged)
        {
            health -= damage;
            healthBar.SetHealth(health);
            anim.SetTrigger("Damage");
            am.Play("Player_Hit");

            if (health <= 0)
            {
                //death
                Die();
            }
        }       
    }

    void Die()
    {
        anim.SetBool("Dead", true);
        GetComponent<PlayerMovement>().canMove = false;
        GetComponent<PlayerMovement>().canDash = false;
        levelLoader.ReloadCurrentScene(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            anim.SetTrigger("Fallen");
            levelLoader.ReloadCurrentScene(0f);
        }

        if (collision.CompareTag("Door"))
        {
            rangedEnemies = GameObject.FindGameObjectsWithTag("RangeEnemy");
            dashEnemies = GameObject.FindGameObjectsWithTag("MeleeEnemy");

            Door door = collision.GetComponent<Door>();

            if (door && rangedEnemies.Length == 0 && dashEnemies.Length == 0)
            {
                door.OpenDoor();
                levelLoader.LoadNextLevel();
            }
        }
    }
}
