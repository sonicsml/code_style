using System.Collections;
using UnityEngine;

/*нет, предпологалось что есть маршрут по которому какой-то объект, и ексть какой-то стрелок, может быть стрелок катается, 
 * может быть это турелька по нарушителю шмаляет, а может это гонки на выживание, где у всех есть пушки и это дуэльная гонка, 
 * но если и делать класс Bullet, то это толжен быть отдельный класс чисто для метательных частичек, а движение по точкам к ней не относится, 
 * задача не менять логику, а отрефакторить, просто навести порядок в той логике что уже есть
1) Spawner
    4. если создаёте класс Bullet, то он должен быть отдельным, и может зранить в себе свой Rigidbody, и можно либо по свойсту давать к нему доступ,
либо написать метод у пули чтобы задать ей скорость методом
2) Bullet
    2. GetNextPoint - переход к следующему индексу можно заменить одной строкой, если записывать в переменную индекса остаток от деления (%)
ее "текущего значение + 1" (только через инкремент, чтоб избежать магических чисел) на "длину массива". Тогда вы замкнете значение между 0 и максимальным индексом.
    5. инициализацию данных нужно выполнять в методе Awake, а не в Start, так как он выполняется первым, чтобы в остальных методах вы были уверены 
что все данные уже инициализированы
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