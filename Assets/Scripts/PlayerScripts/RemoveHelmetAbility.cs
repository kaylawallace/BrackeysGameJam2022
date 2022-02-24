using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RemoveHelmetAbility : MonoBehaviour
{
    [HideInInspector] public bool removed;
    public GameObject dungeon, room;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveHelmet()
    {
        // play MC helmet anim 
        // play slide anim 
        // set dungeon to inactive and room to active 
    }

    public void OnRemoved(InputAction.CallbackContext context)
    {
        removed = context.action.triggered;
    }
}
