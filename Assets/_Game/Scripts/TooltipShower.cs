using _Game.Scripts;
using TMPro;
using UnityEngine;

public class TooltipShower : MonoBehaviour
{
    [SerializeField] private GameObject _tooltip;
    [SerializeField] private TextMeshProUGUI _textTooltip;
    [SerializeField] private Finder _finder;
    [SerializeField] private Grabber _grabber;

    private void Awake()
    {
        _finder.onFind += grabable =>
        {
            Show(grabable);
        };
        
        _finder.onLost += grabable =>
        {
            Hide();
        };
    }


    private void Hide()
    {
        _tooltip.SetActive(false);
    }

    private void Show(Grabable obj)
    {
        if (!_tooltip.activeSelf)
        {
            _tooltip.SetActive(true);
            _textTooltip.text = obj.name;
        }
    }
}