using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType type;
    public enum KeyType
    {
        Purple, 
        Blue
    }

    public KeyType GetKeyType()
    {
        return type;
    }
}
