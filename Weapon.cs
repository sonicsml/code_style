using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.Rigidbody))]

public class Weapon : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletPrefab; 
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _timeWaitShooting = 0.5f;
    [SerializeField] private Transform _target;

    private void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    private IEnumerator SpawnBullet()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeWaitShooting);

        while (enabled)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Rigidbody newBullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);
            newBullet.velocity = direction * _speed; 

            yield return wait;
        }
    }
}