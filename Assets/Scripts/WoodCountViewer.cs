using TMPro;
using UnityEngine;

[RequireComponent(typeof(House))]
public class WoodCountViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _woodCountText;

    private House _house;

    private void Awake()
    {
        _house = GetComponent<House>();
    }

    private void OnEnable()
    {
        _house.WoodCountChanged += ChangeWoodCount;
    }

    private void OnDisable()
    {
        _house.WoodCountChanged -= ChangeWoodCount;
    }

    private void ChangeWoodCount(int count)
    {
        _woodCountText.text = count.ToString();
    }
}
