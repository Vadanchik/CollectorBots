using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _poolMaxSize = 5;
    [SerializeField] private int _poolCapacity = 5;

    protected ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Create(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    protected virtual void PoolRealese(T obj)
    {
        _pool.Release(obj);
    }

    protected abstract T Create();

    protected abstract void ActionOnGet(T obj);
}
