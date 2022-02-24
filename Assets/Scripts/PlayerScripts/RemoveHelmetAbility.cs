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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (removed)
        {
            RemoveHelmet();
            

            if (!cooling)
            {
               
            }
        }

        //print(cooling);
            
    }

    public void RemoveHelmet()
    {
        // play MC helmet anim 
        // play slide anim 
        // set dungeon to inactive and room to active 

        print("taking helmet off");
        transitionAnimator.SetTrigger("helmet_off");
        dungeon.SetActive(false);
        room.SetActive(true);

        StartCoroutine(Cooldown(5f));
    }

    public void PutHelmetBackOn()
    {

        print("putting helmet back on");
        transitionAnimator.SetTrigger("helmet_on");
        room.SetActive(false);
        dungeon.SetActive(true);
    }

    public void OnRemoved(InputAction.CallbackContext context)
    {
        removed = context.action.triggered;
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
}
