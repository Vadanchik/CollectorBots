using System;
using UnityEngine;

public class WoodCounter : MonoBehaviour
{
    private int _count;

    public event Action<int> CountChanged;

    public void AddWood()
    {
        _count++;
        CountChanged?.Invoke(_count);
    }
}
