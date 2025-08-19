using System;
using UnityEngine;

public class Wood : MonoBehaviour
{
    private bool _isDetected = false;

    public event Action Detected;
    public event Action Spawned;
    public event Action<Wood> Collected;

    public bool IsDetected => _isDetected;

    public void Init(Vector3 position)
    {
        transform.position = position;
        _isDetected = false;
        Spawned?.Invoke();
    }

    public void Detect()
    {
        Detected?.Invoke();
        _isDetected = true;
    }

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}
