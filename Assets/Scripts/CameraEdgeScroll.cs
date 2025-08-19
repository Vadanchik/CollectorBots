using UnityEngine;

public class CameraEdgeScroll : MonoBehaviour
{
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private Vector2 panLimitMin;
    [SerializeField] private Vector2 panLimitMax;

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x >= 0 && mousePos.x < panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }

        if (mousePos.x <= Screen.width && mousePos.x > Screen.width - panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        if (mousePos.y >= 0 && mousePos.y < panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (mousePos.y <= Screen.height && mousePos.y > Screen.height - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, panLimitMin.x, panLimitMax.x);
        pos.z = Mathf.Clamp(pos.z, panLimitMin.y, panLimitMax.y);

        transform.position = pos;
    }
}
