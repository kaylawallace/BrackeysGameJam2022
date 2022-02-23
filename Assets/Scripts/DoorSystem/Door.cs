using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Key.KeyType key; 

    public Key.KeyType GetKeyType()
    {
        return key;
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
