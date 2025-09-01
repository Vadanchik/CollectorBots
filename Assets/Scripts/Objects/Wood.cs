using System;
using UnityEngine;

[RequireComponent(typeof(IconViewer))]
public class Wood : MonoBehaviour
{
    private IconViewer _iconViewer;

    public event Action<Wood> Collected;

    public IconViewer IconViewer => _iconViewer;

    private void Awake()
    {
        _iconViewer = GetComponent<IconViewer>();
    }

    public void Init(Vector3 position)
    {
        transform.position = position;
    }

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}
