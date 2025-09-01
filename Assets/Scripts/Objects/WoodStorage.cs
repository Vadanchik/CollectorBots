using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WoodStorage : MonoBehaviour
{
    [SerializeField] private WoodSpawner _woodSpawner;
    [SerializeField] private LayerMask _woodLayer;

    private List<Wood> _woods = new List<Wood>();
    private Queue<Wood> _detectedWood = new Queue<Wood>();

    public int DetectedWoodCount => _detectedWood.Count;

    public Wood GetDetectedWood()
    {
        return _detectedWood.Dequeue();
    }

    public void AddScanedWood()
    {
        foreach (Wood wood in _woods)
        {
            if (_detectedWood.Contains(wood) == false)
            {
                _detectedWood.Enqueue(wood);
                wood.IconViewer.ShowIcon();
            }
        }

        _woods.Clear();
    }

    public void FindWood()
    {
        int maxWoodCount = 10;
        float halfScanBoxHeight = 5;
        int radarRadius = 5;

        Vector3 halfExtents = new Vector3(Utils.TileSize * radarRadius, halfScanBoxHeight, Utils.TileSize * radarRadius);

        Collider[] colliders = new Collider[maxWoodCount];
        int count = Physics.OverlapBoxNonAlloc(Vector3.zero, halfExtents, colliders, Quaternion.identity, _woodLayer);

        _woods = colliders
            .Where(collider => collider != null)
            .Select(collider => collider.GetComponent<Wood>())
            .Where(wood => wood != null)
            .OrderBy(wood => (wood.transform.position - transform.position).magnitude)
            .ToList();
    }
}
