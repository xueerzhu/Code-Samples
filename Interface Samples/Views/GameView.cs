using System;
using System.Collections.Generic;
using R3;
using Ricimi;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameView : View<IGameViewModel>
{
    [Title("Stats")] [SerializeField] private Animator heartAnimator;

    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI levelTitleText;
    [SerializeField] private CircularProgressBar waveTimer;
    [SerializeField] private TopBarView topBarView;

    [Title("Buildings")] [SerializeField] private GameObject buildingBarHorizontalRoot;
    [SerializeField] private Transform buildingBarHorizontalList;
    [SerializeField] private GameObject buildingBarVerticalRoot;
    [SerializeField] private Transform buildingBarVerticalList;
    [SerializeField] private BuildingItemView buildingItemViewPrefab;
    [SerializeField] private bool useVerticalBuildingBar;

    [Title("Cards")] [SerializeField] private DeckWidgetView deckWidgetView;
    [SerializeField] private HandWidgetView handWidgetView;
    [SerializeField] private SpellWidgetView spellWidgetView;

    [FormerlySerializedAs("PerkCardViews")] [SerializeField]
    private List<PerkCardView> perkCardViews;

    [Title("Screens")] [SerializeField] private PauseScreenView pauseScreenView;

    [SerializeField] private GameLoseView losePopup;
    [SerializeField] private GameWinView winPopup;

    [NonSerialized] private readonly List<BuildingItemView> _inventoryItemViews = new();
    [NonSerialized] private CompositeDisposable _disposables = new();
    [NonSerialized] private IGameViewModel _viewModel;

    public bool UseVerticalBuildingBar => useVerticalBuildingBar;


    public override void Init(IGameViewModel gameViewModel)
    {
        _viewModel = gameViewModel;
        losePopup.Init(_viewModel.GameLoseViewModel);
        winPopup.Init(_viewModel.GameWinViewModel);

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        // screens
        _viewModel.IsPaused.Subscribe(newValue => pauseScreenView.SetActive(newValue));
        if (pauseScreenView.IsActive)
            pauseScreenView.Bind(_viewModel.PauseScreenViewModel, this);

        // inventory - buildings old, set to disabled
        //buildingBarVerticalRoot.SetActive(useVerticalBuildingBar);
        //buildingBarHorizontalRoot.SetActive(!useVerticalBuildingBar);
        buildingBarVerticalRoot.SetActive(false);
        buildingBarHorizontalRoot.SetActive(false);
        /*foreach (var viewModel in _viewModel.UnlockedBuildingViewModels)
        {
            var view = Instantiate(buildingItemViewPrefab,
                useVerticalBuildingBar ? buildingBarVerticalList : buildingBarHorizontalList);
            view.Bind(viewModel, this);
            _inventoryItemViews.Add(view);
        }*/

        // inventory - cards
        deckWidgetView.gameObject.SetActive(true);
        handWidgetView.gameObject.SetActive(true);
        deckWidgetView.Init(_viewModel.DeckViewModel, this);
        handWidgetView.Init(_viewModel.HandWidgetViewModel);
        spellWidgetView.Init(_viewModel.SpellWidgetViewModel);

        // win/lose screen
        pauseScreenView.SetActive(false);
        winPopup.gameObject.SetActive(false);
        losePopup.gameObject.SetActive(false);
        EventManager.Instance.AddListener<GameFailedEvent>(OnGameFailed);
        EventManager.Instance.AddListener<GameWonEvent>(OnGameWon);
        /*foreach (var perkCardView in perkCardViews) // todo: use instantiate instead
            perkCardView.Bind(new PerkCardViewModel(_perkManager));*/

        // stats
        topBarView.Init(_viewModel.TopBarViewModel);

        // top bar
        //BindWave(_levelManager.SpawnController);

        waveTimer.gameObject.SetActive(false);
        var waveTimerRect = waveTimer.GetComponent<RectTransform>();
        //waveTimerRect.localPosition = Utility.WorldPositionToUILocalPosition(_levelManager.InitialSpawnPoint.transform.position, waveTimerRect)
        // + new Vector2(0, waveTimerRect.rect.height);

        // game states
        /*TlGameManager.Instance.CurrentLevelId.Subscribe(newValue => { levelTitleText.text = "Level " + newValue; })
            .AddTo(_disposables);*/
    }

    /*
     * Invalid operations such as attempting to use a disposed object typically throw an exception at runtime. However, the CompositeDisposable class in the System.Reactive namespace, which is commonly used in Reactive Extensions, is designed to be tolerant to such misuse.
When you call the Add or AddTo method on a CompositeDisposable after it has been disposed, instead of throwing an ObjectDisposedException, it immediately disposes of the new disposable that you're trying to add. It effectively becomes a sink for disposables where they get immediately disposed.
This design choice is likely due to real-world usage patterns of CompositeDisposable. Perhaps it's considered more valuable to ensure that disposables are, indeed, disposed rather than to prompt developers with an exception when they add to a CompositeDisposable that has already been disposed.
This behaviour means that, while calling _disposables = new CompositeDisposable(); after _disposables.Dispose(); might not be strictly necessary to prevent an ObjectDisposedException in this case, it's still an essential part of correctly using CompositeDisposable. Without it, any new subscriptions you add will be immediately disposed, essentially making them useless.
So while you did not see an error, not re-instantiating a new CompositeDisposable would still cause logical issues in your code, where new subscriptions would not work as intended. So it's important to always make sure that your CompositeDisposable is ready and able to hold any new disposables.
     */

    public override void Dispose()
    {
        _disposables.Dispose();
        _disposables = new CompositeDisposable();
    }

    // todo: wave ui: refactor to use ViewModel
    /*private void BindWave(EnemySpawnController spawnController)
    {
        spawnController.TotalWaves.Subscribe(newValue =>
        {
            waveText.text = $"WAVE  {spawnController.CurrentWave.Value} / {spawnController.TotalWaves.Value}";
        }).AddTo(_disposables);

        spawnController.CurrentWave.Subscribe(newValue =>
        {
            waveText.text = $"WAVE  {spawnController.CurrentWave.Value} / {spawnController.TotalWaves.Value}";
        }).AddTo(_disposables);

        spawnController.WaveTimerPercentage.Subscribe(newValue => { waveTimer.Percentage = newValue; })
            .AddTo(_disposables);

        spawnController.IsBuildPhase.Subscribe(newValue => { waveTimer.gameObject.SetActive(newValue); })
            .AddTo(_disposables);
    }*/


    private void OnGameWon(GameWonEvent e)
    {
        winPopup.SetActive(true);
    }

    private void OnGameFailed(GameFailedEvent e)
    {
        losePopup.SetActive(true);
    }
}