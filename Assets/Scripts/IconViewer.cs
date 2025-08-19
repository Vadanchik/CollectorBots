using UnityEngine;

[RequireComponent(typeof(Wood))]
public class IconViewer : MonoBehaviour
{
    [SerializeField] private Transform _icon;

    private Transform _mainCamera;
    private Wood _wood;

    private void Awake()
    {
        _wood = GetComponent<Wood>();
        _wood.Detected += Activate;
        _wood.Spawned += Deactivate;
    }

    private void Start()
    {
        _mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        _icon.rotation = _mainCamera.rotation;
    }

    private void Activate()
    {
        _icon.gameObject.SetActive(true);
    }

    private void Deactivate()
    {
        _icon.gameObject.SetActive(false);
    }
}