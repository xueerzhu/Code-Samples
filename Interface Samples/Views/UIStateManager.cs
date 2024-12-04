using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UIStateManager : Singleton<UIStateManager>
{
    [SerializeField] private float disabledAlpha = 0.4f;

    [FormerlySerializedAs("deckWidgetGroup")] [SerializeField]
    private CanvasGroup drawPileWidgetGroup;

    [SerializeField] private CanvasGroup handWidgetGroup;
    [SerializeField] private CanvasGroup spellWidgetGroup;

    [NonSerialized] private GameData _gameData;

    public bool HandWidgetGroupUIState => handWidgetGroup.interactable;

    private void Start()
    {
        EventManager.Instance.AddListener<ToggleBuildingModeEvent>(OnToggleBuildingMode);
        EventManager.Instance.AddListener<ToggleMachineModeEvent>(OnToggleMachineMode);
        EventManager.Instance.AddListener<AttackPhaseStartedEvent>(OnAttackPhaseStarted);
        EventManager.Instance.AddListener<AttackPhaseEndedEvent>(OnAttackPhaseEnded);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener<ToggleBuildingModeEvent>(OnToggleBuildingMode);
    }

    public void Init()
    {
        IDataService dataService = ServiceLocator.Instance.Resolve<IDataService>();
        _gameData = dataService.GameData;
        SetGroupState(drawPileWidgetGroup, true);
    }

    private void OnToggleBuildingMode(ToggleBuildingModeEvent evt)
    {
        //SetGroupState(spellWidgetGroup, evt.InBuildingMode);
        SetGroupState(handWidgetGroup, evt.InBuildingMode);
    }

    private void OnToggleMachineMode(ToggleMachineModeEvent evt)
    {
        if (_gameData.CanBuildDuringAttack)
            //SetGroupState(drawPileWidgetGroup, evt.InMachineMode);
            SetGroupState(handWidgetGroup, evt.InMachineMode);
        //SetGroupState(spellWidgetGroup, evt.InMachineMode);
    }

    private void OnAttackPhaseStarted(AttackPhaseStartedEvent evt)
    {
        if (!_gameData.CanBuildDuringAttack)
            //SetGroupState(drawPileWidgetGroup, true);
            SetGroupState(handWidgetGroup, true);
        //SetGroupState(spellWidgetGroup, false);
    }

    private void OnAttackPhaseEnded(AttackPhaseEndedEvent evt)
    {
        //SetGroupState(drawPileWidgetGroup, false);
        SetGroupState(handWidgetGroup, false);
        //SetGroupState(spellWidgetGroup, true);
    }

    private void SetAllActionGroupsState(bool disable)
    {
        //SetGroupState(drawPileWidgetGroup, disable);
        SetGroupState(handWidgetGroup, disable);
        //SetGroupState(spellWidgetGroup, disable);
    }

    private void SetGroupState(CanvasGroup group, bool disable)
    {
        group.interactable = !disable;
        group.alpha = disable ? disabledAlpha : 1f;
    }
}