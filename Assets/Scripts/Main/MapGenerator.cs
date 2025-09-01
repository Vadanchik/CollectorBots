using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<Tile> _tiles;
    [SerializeField] private Tile _clearTile;
    [SerializeField] private Vector3 _zeroPosition = Vector3.zero;
    [SerializeField] private int _mapSize;
    [SerializeField] private int _tileSize;
    [SerializeField] private float _delayDifference;
    [SerializeField] private Transform _tileContainer;

    public Vector3 ZeroPosition => _zeroPosition;
    public int MapSize => _mapSize;

    public void Generate()
    {
        Tile tile = null;

        for (int xPos = -_mapSize; xPos <= _mapSize; xPos++)
        {
            for (int zPos = -_mapSize; zPos <= _mapSize; zPos++)
            {
                if (xPos ==0 && zPos == 0)
                {
                    tile = Instantiate(_clearTile);
                }
                else
                {
                    tile = Instantiate(_tiles[UnityEngine.Random.Range(0, _tiles.Count)]);
                }
                
                float delay = xPos + zPos + _mapSize * 2;
                tile.Init(xPos, zPos);
                tile.transform.SetParent(_tileContainer);
            }
        }
    }
}
