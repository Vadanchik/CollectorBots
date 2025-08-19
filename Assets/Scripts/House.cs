using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HouseRadar))]
public class House : MonoBehaviour
{
    [SerializeField] private Transform _botSpawnPoint;
    [SerializeField] private BotSpawner _botSpawner;

    private List<Bot> _bots = new List<Bot>();
    private Queue<Wood> _detectedWoods = new Queue<Wood>();
    private Queue<Bot> _freeBots = new Queue<Bot>();

    private HouseRadar _radar;
    private int _woodCount;

    public event Action<int> WoodCountChanged;

    private void Awake()
    {
        _radar = GetComponent<HouseRadar>();
    }

    private void OnEnable()
    {
        _radar.Scaned += StartGathering;
    }

    private void OnDisable()
    {
        _radar.Scaned -= StartGathering;
    }

    public void Init(Vector3 position, BotSpawner botSpawner)
    {
        transform.position = position;
        _botSpawner = botSpawner;
    }

    public void SpawnBot()
    {
        Bot bot = _botSpawner.Spawn(Utils.RandomDeviation(_botSpawnPoint.position + Utils.OffsetY * Vector3.up, 3));
        bot.Init(this);
        _bots.Add(bot);
        _freeBots.Enqueue(bot);
        bot.Delivered += TakeWood;
    }

    public IEnumerator SendBots()
    {
        float tickDuration = 0.1f;

        WaitForSeconds tick = new WaitForSeconds(tickDuration);

        while (_detectedWoods.Count > 0)
        {
            if (_freeBots.Count > 0)
                _freeBots.Dequeue().StartGathering(_detectedWoods.Dequeue());

            yield return tick;
        }
    }

    private void StartGathering(List<Wood> woods)
    {
        foreach (Wood wood in woods)
        {
            _detectedWoods.Enqueue(wood);
        }

        StartCoroutine(SendBots());
    }

    private void TakeWood(Bot bot)
    {
        _woodCount++;
        _freeBots.Enqueue(bot);

        WoodCountChanged?.Invoke(_woodCount);
    }
}
