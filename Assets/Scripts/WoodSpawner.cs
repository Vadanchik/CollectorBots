using System.Collections;
using UnityEngine;

public class WoodSpawner : PoolSpawner<Wood>
{
    [SerializeField] private float _timeOverSpawn;

    private Vector3 _currentWoodPosition;

    public void StartSpawnWood(int maxPosition)
    {
        StartCoroutine(SpawnWoodOverTime(maxPosition));
    }

    protected override void ActionOnGet(Wood wood)
    {
        wood.Init(_currentWoodPosition);
        wood.gameObject.SetActive(true);
    }

    protected override Wood Create()
    {
        Wood wood = Instantiate(_prefab);
        wood.Collected += PoolRealese;

        return wood;
    }

    private IEnumerator SpawnWoodOverTime(int maxPosition)
    {
        WaitForSeconds wait = new WaitForSeconds(_timeOverSpawn);

        while (enabled)
        {
            yield return wait;

            _currentWoodPosition = new Vector3(Utils.RandomNonZeroInRange(maxPosition) * Utils.TileSize, Utils.OffsetY, Utils.RandomNonZeroInRange(maxPosition) * Utils.TileSize);

            if (_pool.CountActive < _poolMaxSize)
                _pool.Get();
        }
    }
}
