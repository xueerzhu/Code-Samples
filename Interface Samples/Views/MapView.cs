using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using TowerLab.Map;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
public class MapView : MonoBehaviour
{
    [Title("Map Settings")] [SerializeField]
    private RectTransform parentRectTransform; // The parent panel or container for the grid.

    [Required] [SerializeField] private MapNodeView nodePrefab; // The prefab of the grid node.
    [SerializeField] private MapNodeLineView linePrefab;
    [SerializeField] private float nodeSize;

    [NonSerialized] private readonly List<MapNodeLineView> _lines = new();
    [NonSerialized] private MapData _mapData;
    [NonSerialized] private MapGenerator _mapGenerator;
    [NonSerialized] private IMapScreenViewModel _viewModel;

    public List<MapNodeView> NodeViews { get; } = new();

    private void OnEnable()
    {
        _mapGenerator = GetComponent<MapGenerator>();
    }


    public void Init(IMapScreenViewModel viewModel)
    {
        _viewModel = viewModel;
        _mapData = _viewModel.MapData;
        var map = _mapGenerator.GenerateMap(_mapData, parentRectTransform);

        InitView(map);
    }

    public async void SelectRoom(int roomId, int floorId)
    {
        foreach (var node in NodeViews) node.Select(node.FloorId == floorId && node.RoomId == roomId);
        // Wait for 1 sec animation delay
        await UniTask.Delay(1000);

        _viewModel.SelectRoom(roomId, floorId);
    }

    private void HighlightRooms(int floorId)
    {
        foreach (var node in NodeViews) node.Highlight(node.FloorId == floorId);
    }

    private void InitView(Map map)
    {
        SpawnRoomViews(map);
        SpawnLineViews(map);
        StateView(_viewModel.PlayerState.Value);
    }

    private void StateView(State state)
    {
        if (state is SelectARoomState) HighlightRooms(state.FloorToSelectId);
    }

    private void SpawnRoomViews(Map map)
    {
        NodeViews.Clear();
        for (var i = 0; i < map.Depth; i++)
        {
            var currentFloorId = i + 1;
            var roomId = 0; // if a floor has 2 activated rooms, it has room Id 1 and 2

            foreach (var node in map.GetNodesAtFloorIndex(i))
            {
                roomId += 1;
                // Instantiate a new node.
                var newNode = Instantiate(nodePrefab, parentRectTransform);
                newNode.Init(_viewModel, this, currentFloorId, roomId);
                NodeViews.Add(newNode);
                // Set the node's position.
                var newNodeRectTransform = newNode.GetComponent<RectTransform>();
                newNode.SetIcon(node.RoomData.Legend);
                //Debug.Log("node position: " + node.XPosition + ", " + node.YPosition);
                newNodeRectTransform.anchoredPosition = new Vector2(node.XPosition, node.YPosition);
                // Set the node's size.
                newNodeRectTransform.sizeDelta = new Vector2(nodeSize, nodeSize);
            }
        }
    }

    private void SpawnLineViews(Map map)
    {
        foreach (var connection in map.GetAllConnections())
        {
            var line = Instantiate(linePrefab, transform);
            line.Init(connection.Start.Position, connection.End.Position);
            line.SetAnimates(false);
            _lines.Add(line);
        }
    }

    /*private void UpdatePathsAnimation(int lastFinishedLevelId)
    {
        if (_viewModel.CurrentLevelId.Value > lastFinishedLevelId)
        {
            foreach (var line in _lines) line.view.SetAnimates(line.path.StartLevelId == lastFinishedLevelId && line.path.EndLevelId == _viewModel.CurrentLevelId.Value);
        }
        else
        {
            foreach (var line in _lines) line.view.SetAnimates(line.path.StartLevelId == lastFinishedLevelId);
        }
    }*/
}