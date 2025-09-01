using System;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    [SerializeField] private House _prefab;
    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private WoodStorage _woodStorage;

    public House Spawn(Vector3 position)
    {
        House house = Instantiate(_prefab);
        house.Init(position + Utils.OffsetY * Vector3.up, _botSpawner, _woodStorage);

        return house;
    }
}
