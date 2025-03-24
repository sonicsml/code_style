using System.Collections;
using UnityEngine;

/*���, �������������� ��� ���� ������� �� �������� �����-�� ������, � ����� �����-�� �������, ����� ���� ������� ��������, 
 * ����� ���� ��� �������� �� ���������� �������, � ����� ��� ����� �� ���������, ��� � ���� ���� ����� � ��� �������� �����, 
 * �� ���� � ������ ����� Bullet, �� ��� ������ ���� ��������� ����� ����� ��� ����������� ��������, � �������� �� ������ � ��� �� ���������, 
 * ������ �� ������ ������, � �������������, ������ ������� ������� � ��� ������ ��� ��� ����
1) Spawner
    4. ���� ������� ����� Bullet, �� �� ������ ���� ���������, � ����� ������� � ���� ���� Rigidbody, � ����� ���� �� ������� ������ � ���� ������,
���� �������� ����� � ���� ����� ������ �� �������� �������
2) Bullet
    2. GetNextPoint - ������� � ���������� ������� ����� �������� ����� �������, ���� ���������� � ���������� ������� ������� �� ������� (%)
�� "�������� �������� + 1" (������ ����� ���������, ���� �������� ���������� �����) �� "����� �������". ����� �� �������� �������� ����� 0 � ������������ ��������.
    5. ������������� ������ ����� ��������� � ������ Awake, � �� � Start, ��� ��� �� ����������� ������, ����� � ��������� ������� �� ���� ������� 
��� ��� ������ ��� ����������������
*/
[RequireComponent(typeof(Rigidbody))]

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Mover _bulletPrefab;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private Transform _target;

    private void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    private IEnumerator SpawnBullet()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeWaitShooting);
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        while (enabled)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Mover newBullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);
            rigidbody = newBullet.GetComponent<Rigidbody>();

            rigidbody.transform.up = direction;
            rigidbody.velocity = direction * _speed;

            yield return wait;
        }
    }
}