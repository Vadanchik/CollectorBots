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
        StartCoroutine(GoToWood(wood));
    }

    private IEnumerator GoToWood(Wood wood)
    {
        WaitForEndOfFrame tick = new WaitForEndOfFrame();

        while ((wood.transform.position - transform.position).magnitude > _gatherDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, wood.transform.position, _speed * Time.deltaTime);

            yield return tick;
        }

        StartCoroutine(GatherWood(wood));
    }

    private IEnumerator GoToHouse()
    {
        WaitForEndOfFrame tick = new WaitForEndOfFrame();

        while ((_house.transform.position - transform.position).magnitude > _gatherDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, _house.transform.position, _speed * Time.deltaTime);

            yield return tick;
        }

        Delivered?.Invoke(this);
    }
    
    private IEnumerator GatherWood(Wood wood)
    {
        WaitForSeconds time = new WaitForSeconds(_timeToGather);

        yield return time;

        wood.Collect();
        StartCoroutine(GoToHouse());
    }
}
