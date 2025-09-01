using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private HouseSpawner _houseSpawner;
    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private WoodSpawner _woodSpawner;

    [SerializeField] private int _starterBotCount;

    private void Start()
    {
        _mapGenerator.Generate();
        House house = _houseSpawner.Spawn(Vector3.zero);
        SpawnStarterBots(house);
        _woodSpawner.StartSpawnWood(_mapGenerator.MapSize);
    }

    private void SpawnStarterBots(House starterHouse)
    {
        for (int i = 0; i < _starterBotCount; i++)
        {
            starterHouse.SpawnBot();
        }
    }
}
