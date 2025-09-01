using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HouseRadar))]
[RequireComponent(typeof(WoodCounter))]
public class House : MonoBehaviour
{
    [SerializeField] private Transform _botSpawnPoint;
    [SerializeField] private BotSpawner _botSpawner;

    private Queue<Bot> _freeBots = new Queue<Bot>();

    private HouseRadar _radar;
    private WoodStorage _woodStorage;
    private WoodCounter _woodCounter;

    public HouseRadar Radar => _radar;

    private void Awake()
    {
        _radar = GetComponent<HouseRadar>();
        _woodCounter = GetComponent<WoodCounter>();
    }

    private void OnEnable()
    {
        _radar.Scaned += ExecuteBotSending;
    }

    private void OnDisable()
    {
        _radar.Scaned -= ExecuteBotSending;
    }

    public void Init(Vector3 position, BotSpawner botSpawner, WoodStorage woodStorage)
    {
        transform.position = position;
        _botSpawner = botSpawner;
        _woodStorage = woodStorage;
    }

    public void SpawnBot()
    {
        Bot bot = _botSpawner.Spawn(Utils.RandomDeviation(_botSpawnPoint.position + Utils.OffsetY * Vector3.up, 3));
        bot.Init(this.transform.position);
        _freeBots.Enqueue(bot);
        bot.Delivered += TakeWood;
    }

    public IEnumerator SendBots()
    {
        float tickDuration = 0.1f;

        WaitForSeconds tick = new WaitForSeconds(tickDuration);

        while (_woodStorage.DetectedWoodCount > 0)
        {
            if (_freeBots.Count > 0)
                _freeBots.Dequeue().StartGathering(_woodStorage.GetDetectedWood());

            yield return tick;
        }
    }

    private void ExecuteBotSending()
    {
        _woodStorage.AddScanedWood();

        StartCoroutine(SendBots());
    }

    private void TakeWood(Bot bot)
    {
        _woodCounter.AddWood();
        _freeBots.Enqueue(bot);
    }
}
