using UnityEngine;

public class Tile : MonoBehaviour 
{
    public void Init(int xPos, int zPos)
    {
        transform.position = new Vector3(xPos * Utils.TileSize, 0, zPos * Utils.TileSize);
    }
}

