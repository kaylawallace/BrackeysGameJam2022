using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keys;
    LevelLoader loader;

    public Image keyUI;

    private void Awake()
    {
        keys = new List<Key.KeyType>();
        loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    public void AddKey(Key.KeyType key)
    {
        keyUI.gameObject.SetActive(true);
        keys.Add(key);
    }

    public void RemoveKey(Key.KeyType key)
    {
        keyUI.gameObject.SetActive(false);
        keys.Remove(key);
    }

    public bool ContainsKey(Key.KeyType key)
    {
        return keys.Contains(key);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Key key = collision.GetComponent<Key>();

        if (key)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        Door door = collision.GetComponent<Door>();

        if (door)
        {
            if (ContainsKey(door.GetKeyType()))
            {
                door.OpenDoor();
                RemoveKey(door.GetKeyType());
                loader.LoadNextLevel();
            }
        }
    }
}
