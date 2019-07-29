using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour, IScoreView
{
    [SerializeField] private Text pathField = null;
    [SerializeField] private Text crystalField = null;

    public int Path
    {
        set => pathField.text = $"Path = {value}";
    }

    public int CrystalCount
    {
        set => crystalField.text =  $"Crystals = {value}";
    }
}