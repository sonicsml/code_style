using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private Transform _target;

    private void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    private IEnumerator SpawnBullet()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeWaitShooting);
        Rigidbody _rigidbody = GetComponent<Rigidbody>();

        while (enabled)
        {
            var direction = (_target.position - transform.position).normalized;
            Bullet newBullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);

            newBullet.GetComponent<Rigidbody>().transform.up = direction;
            newBullet.GetComponent<Rigidbody>().velocity = direction * _speed;

            yield return wait;
        }
    }
}