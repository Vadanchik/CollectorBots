using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private Bot _prefab;

    public Bot Spawn(Vector3 position)
    {
        Bot bot = Instantiate(_prefab, position, Quaternion.identity);

        return bot;
    }
}
