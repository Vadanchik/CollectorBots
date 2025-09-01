using TMPro;
using UnityEngine;

[RequireComponent(typeof(WoodCounter))]
public class WoodCountViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _woodCountText;

    private WoodCounter _counter;

    private void Awake()
    {
        _counter = GetComponent<WoodCounter>();
    }

    private void OnEnable()
    {
        _counter.CountChanged += ChangeWoodCount;
    }

    private void OnDisable()
    {
        _counter.CountChanged -= ChangeWoodCount;
    }

    private void ChangeWoodCount(int count)
    {
        _woodCountText.text = count.ToString();
    }
}
