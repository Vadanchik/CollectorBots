using UnityEngine;

public class CameraEdgeScroll : MonoBehaviour
{
    [SerializeField] private float _panSpeed = 20f;
    [SerializeField] private float _panBorderThickness = 10f;
    [SerializeField] private Vector2 _panLimitMin;
    [SerializeField] private Vector2 _panLimitMax;

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x >= 0 && mousePos.x < _panBorderThickness)
        {
            pos.z += _panSpeed * Time.deltaTime;
        }

        if (mousePos.x <= Screen.width && mousePos.x > Screen.width - _panBorderThickness)
        {
            pos.z -= _panSpeed * Time.deltaTime;
        }

        if (mousePos.y >= 0 && mousePos.y < _panBorderThickness)
        {
            pos.x -= _panSpeed * Time.deltaTime;
        }

        if (mousePos.y <= Screen.height && mousePos.y > Screen.height - _panBorderThickness)
        {
            pos.x += _panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, _panLimitMin.x, _panLimitMax.x);
        pos.z = Mathf.Clamp(pos.z, _panLimitMin.y, _panLimitMax.y);

        transform.position = pos;
    }
}
