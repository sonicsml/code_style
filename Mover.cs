using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _places;
    private Transform _allPlacespoint;
    private int _numberOfPlace;

    private void Awake()
    {
        _places = new Transform[_allPlacespoint.childCount];

        for (int i = 0; i < _allPlacespoint.childCount; i++)
        {
            _places[i] = _allPlacespoint.GetChild(i)   ;
        }
    }

    private void Update()
    {
        Transform pointByNumberInArray = _places[_numberOfPlace];

        transform.position = Vector3.MoveTowards(transform.position, pointByNumberInArray.position, _speed * Time.deltaTime);

        float minDistance = 0.01f;

        if ((transform.position - pointByNumberInArray.position).sqrMagnitude < minDistance * minDistance)
        {
            transform.position = GetNextPoint();
        }
    }

    private Vector3 GetNextPoint()
    {
        _numberOfPlace++;

        if (_numberOfPlace == _places.Length)
        {
            _numberOfPlace = 0;
        }

        Vector3 direction = _places[_numberOfPlace].transform.position;

        transform.forward = direction - transform.position;

        return direction;
    }
}