using UnityEngine;

public static class Utils
{
    public const float OffsetY = 1.3f;
    public const float TileSize = 15;

    public static Vector3 RandomDeviation(Vector3 position, float radius)
    {
        return position + Random.Range(0, radius) * new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized;
    }

    public static int RandomNonZeroInRange(int maxInclusive)
    {
        int randomNumber = Random.Range(1, maxInclusive + 1);
        return Random.Range(0, 2) == 0 ? randomNumber : -randomNumber;
    }
}
