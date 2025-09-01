using System.Collections.Generic;
using UnityEngine;

public class WoodStorage : MonoBehaviour
{
    [SerializeField] private WoodSpawner _woodSpawner;

    private List<Wood> _spawnedWood = new List<Wood>();
    private Queue<Wood> _detectedWood = new Queue<Wood>();

    public int DetectedWoodCount => _detectedWood.Count;

    private void OnEnable()
    {
        _woodSpawner.Spawned += AddWood;
    }

    private void OnDisable()
    {
        _woodSpawner.Spawned -= AddWood;
    }

    public Wood GetDetectedWood()
    {
        return _detectedWood.Dequeue();
    }

    public void AddScanedWood()
    {
        foreach (Wood wood in _spawnedWood)
        {
            _detectedWood.Enqueue(wood);
            wood.IconViewer.ShowIcon();
        }

        _spawnedWood.Clear();
    }

    private void AddWood(Wood wood)
    {
        _spawnedWood.Add(wood);
        wood.IconViewer.HideIcon();
        Debug.Log(_spawnedWood.Count);
    }
}
