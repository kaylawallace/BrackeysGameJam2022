using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public LevelLoader loader;
    public Image dialogueBox;
    public PlayerMovement plr; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueBox.gameObject.SetActive(true);
            plr.canDash = false;
            plr.canMove = false; 
            loader.EndGame();
        }
    }
}
