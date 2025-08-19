using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseRadar : MonoBehaviour
{
    [SerializeField] private float _cooldownTime;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private LayerMask _woodLayer;

    private List<Wood> _woods = new List<Wood>();
    private int _radarRadius = 5;
    private bool _isReady = true;

    public event Action<List<Wood>> Scaned;

    private void OnMouseDown()
    {
        if (_isReady == true)
        {
            _isReady = false;
            _effect.Play();
            Scan();
            StartCoroutine(ExecuteCooldown());
        }
    }

    private void Scan()
    {
        int maxWoodCount = 10;
        float halfScanBoxHeight = 5;
        Vector3 halfExtents = new Vector3(Utils.TileSize * _radarRadius, halfScanBoxHeight, Utils.TileSize * _radarRadius);

        Collider[] colliders = new Collider[maxWoodCount];
        int count = Physics.OverlapBoxNonAlloc(Vector3.zero, halfExtents, colliders, Quaternion.identity, _woodLayer);

        _woods = colliders
            .Where(collider => collider != null)
            .Select(collider => collider.GetComponent<Wood>())
            .Where(wood => wood != null)
            .Where(wood => wood.IsDetected == false)
            .OrderBy(wood => (wood.transform.position - transform.position).magnitude)
            .ToList();

        foreach (Wood wood in _woods)
        {
            wood.Detect();
        }

        Scaned?.Invoke(_woods);
    }

    private IEnumerator ExecuteCooldown()
    {
        WaitForEndOfFrame tick = new WaitForEndOfFrame();
        float timer = _cooldownTime;

        while (timer > 0)
        {
            yield return tick;

            timer = Mathf.Clamp(timer - Time.deltaTime, 0, _cooldownTime);
        }

        _isReady = true;
    }
}