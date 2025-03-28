using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _places;
    private Transform _allPlacespoint;
    private int _numberOfPlace;

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void Awake()
    {
        _places = new Transform[_allPlacespoint.childCount];

        for (int i = 0; i < _places.Length; i++)
        {
            _places[i] = _allPlacespoint.GetChild(i);
        }
    }
#endif

    private void Update()
    {
        Transform currentPoint = _places[_numberOfPlace];
        float minDistance = 0.01f;

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, _speed * Time.deltaTime);

        if ((transform.position - currentPoint.position).sqrMagnitude < minDistance * minDistance)
        {
            transform.position = GetNextPoint();
        }
    }

    private Vector3 GetNextPoint()
    {
        _numberOfPlace = ++_numberOfPlace % _places.Length;

        Vector3 direction = _places[_numberOfPlace].transform.position;

        transform.forward = direction - transform.position;

        return direction;
    }
}