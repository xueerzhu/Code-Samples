using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapNodeView : MonoBehaviour
{
    [Required] [SerializeField] private Image icon;
    [SerializeField] private Button button;
    [SerializeField] private Color notSelectedColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private TextMeshProUGUI levelIdText;
    [SerializeField] private Image highlightIcon;
    [NonSerialized] private Animator _highlightAnimator;
    [NonSerialized] private MapView _mapView;

    [NonSerialized] private IMapScreenViewModel _viewModel;
    public int FloorId { get; private set; }
    public int RoomId { get; private set; }

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    public void Init(IMapScreenViewModel viewModel, MapView mapView, int floorId, int roomId)
    {
        _viewModel = viewModel;
        button.onClick.AddListener(OnNodeClick);
        FloorId = floorId;
        RoomId = roomId;
        _mapView = mapView;

        InitView();
    }


    public void Highlight(bool active)
    {
        if (!active)
        {
            highlightIcon.gameObject.SetActive(false);
        }
        else
        {
            highlightIcon.gameObject.SetActive(true);
            AnimateHighlight(_highlightAnimator);
        }
    }

    public void Select(bool active)
    {
        if (active)
            StopAnimateHighlight(_highlightAnimator);
        else
            highlightIcon.gameObject.SetActive(false);
    }

    public void Dispose()
    {
        button?.onClick.RemoveAllListeners();
    }

    private void AnimateHighlight(Animator animator)
    {
        animator.enabled = true;
        animator.Play("Heart-Idle");
    }

    private void StopAnimateHighlight(Animator animator)
    {
        animator.StopPlayback();
        animator.enabled = false;
    }

    private void InitView()
    {
        // this would stop the animator from playing animations
        // Note - It doesn't stop a currently playing animation
        _highlightAnimator = highlightIcon.GetComponent<Animator>();
        _highlightAnimator.enabled = false;

        levelIdText.text = FloorId == 0 ? "Start" : FloorId.ToString();
        Highlight(false);
    }


    private void OnNodeClick()
    {
        var selected = _viewModel.TrySelectLevel(RoomId, FloorId);
        if (selected) _mapView.SelectRoom(RoomId, FloorId);
    }
}