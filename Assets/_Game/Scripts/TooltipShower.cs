using _Game.Scripts;
using TMPro;
using UnityEngine;

public class TooltipShower : MonoBehaviour
{
    [SerializeField] private GameObject _tooltip;
    [SerializeField] private TextMeshProUGUI _textTooltip;
    [SerializeField] private PickupController _pickupController;

    private void Awake()
    {
        _pickupController.onFindPickupable += Show;
        _pickupController.onHoldPickupable += Hide;
        _pickupController.onLostPickupable += Hide;
        Hide();
    }

    private void Hide()
    {
        _tooltip.SetActive(false);
    }

    private void Show(Pickupable obj)
    {
        if (!_tooltip.activeSelf)
        {
            _tooltip.SetActive(true);
            _textTooltip.text = obj.name;
        }
    }
}