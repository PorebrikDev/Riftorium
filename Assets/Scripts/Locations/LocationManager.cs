using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LocationManager : MonoBehaviour
{
    public Transform CurrentLocation => _currentLocation.transform;

    [SerializeField] private GameObject _startLocation;
    [SerializeField] private GameObject _currentLocation;

    [Inject] private Player _player;
    [Inject] private readonly UiAnimatorScript _uiAnimator;


    private readonly Dictionary<SpawnEnumID, Transform> _spawns = new();

    private void Awake()
    {
        _currentLocation = _startLocation;
    }

    public void Register(SpawnEnumID id, Transform point)
    {
        if (_spawns.ContainsKey(id))
        {
            Debug.LogError($"Duplicate SpawnEnumID: {id}");
            return;
        }

        _spawns.Add(id, point);
    }

    public void Unregister(SpawnEnumID id)
    {
        if (_spawns.ContainsKey(id))
            _spawns.Remove(id);
    }

    public void SwitchLocation(GameObject newLocation, SpawnEnumID spawnId)
    {
        if (_currentLocation != null)
            _currentLocation.SetActive(false);

        _currentLocation = newLocation;
        _currentLocation.SetActive(true);

        if (_spawns.TryGetValue(spawnId, out var spawn))
        {
            Debug.Log(_uiAnimator);
            _uiAnimator.PlayAnimation(UIAnimatorEnum.GoBlackScreen);
            _player.transform.position = spawn.position;
            _player.CurrentTransPos = spawn.position;
        }
        else
        {
            Debug.LogError($"Spawn not found: {spawnId}");
        }
    }
}