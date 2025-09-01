using UnityEngine;

public class IconViewer : MonoBehaviour
{
    [SerializeField] private Transform _icon;

    private Transform _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        _icon.rotation = _mainCamera.rotation;
    }

    public void ShowIcon()
    {
        _icon.gameObject.SetActive(true);
    }

    public void HideIcon()
    {
        _icon.gameObject.SetActive(false);
    }
}