using DG.Tweening;
using UnityEngine;
using Zenject;

public class Droped : MonoBehaviour
{
    private bool _isFull = true;

    [SerializeField] private ItemSO _itemSo;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _amount;
    [SerializeField] private int _countObject = 1;

    [SerializeField] private Transform[] _dropPositions;

    [Inject] LocationManager _locationManager;
    [Inject] private DiContainer _container;


    private Transform _parent;
    private void Awake()
    {
        _parent = transform;
    }

    public void DropThis()
    {
        if (!_isFull) return;

        for (int i = 0; i < _countObject; i++)
        {
            GameObject obj = _container.InstantiatePrefab(
                _prefab,
                _parent.position,
                Quaternion.identity,
                null
            );

            Transform dropPosition = null;

            if (_dropPositions != null && _dropPositions.Length > 0)
            {
                dropPosition = _dropPositions[i % _dropPositions.Length];
            }

            Vector2 direction;

            if (dropPosition != null)
            {
                direction = (dropPosition.position - _parent.position).normalized;
            }
            else
            {
                direction = Random.insideUnitCircle.normalized;
            }

            float distance = Random.Range(1.5f, 3f);

            obj.GetComponent<PickUpObject>().Init(_itemSo, _amount);

            obj.transform.SetParent(_locationManager.CurrentLocation);

            obj.transform.DOJump(
                obj.transform.position + (Vector3)(direction * distance),
                1f,
                1,
                0.5f
            );
        }

        _isFull = false;
    }

    public void ResetDrop()
    {
        _isFull = true;
    }

}
