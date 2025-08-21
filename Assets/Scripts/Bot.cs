using System;
using System.Collections;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToGather;

    private House _house;
    private float _gatherDistance = 5;

    public event Action<Bot> Delivered;

    public void Init(House house)
    {
        _house = house;
    }

    public void StartGathering(Wood wood)
    {
        StartCoroutine(Run(wood));
    }

    private IEnumerator Run(Wood wood)
    {
        yield return GoToTarget(wood.transform.position);

        yield return GatherWood(wood);

        yield return GoToTarget(_house.transform.position);

        Delivered?.Invoke(this);
    }

    private IEnumerator GoToTarget(Vector3 position)
    {
        WaitForEndOfFrame tick = new WaitForEndOfFrame();

        while ((position - transform.position).magnitude > _gatherDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);

            yield return tick;
        }
    }
    
    private IEnumerator GatherWood(Wood wood)
    {
        WaitForSeconds time = new WaitForSeconds(_timeToGather);

        yield return time;

        wood.Collect();
    }
}
