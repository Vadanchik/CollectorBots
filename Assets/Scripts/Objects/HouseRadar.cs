using System;
using System.Collections;
using UnityEngine;

public class HouseRadar : MonoBehaviour
{
    [SerializeField] private float _cooldownTime;
    [SerializeField] private ParticleSystem _effect;

    private bool _isReady = true;

    public event Action Scaned;

    private void OnMouseDown()
    {
        if (_isReady == true)
        {
            _isReady = false;
            _effect.Play();
            Scaned?.Invoke();
            StartCoroutine(ExecuteCooldown());
        }
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