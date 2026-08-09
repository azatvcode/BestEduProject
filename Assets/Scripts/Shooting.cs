using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [Tooltip("Камера, от которой будет вылетать луч выстрела")]
    public Camera cam;

    [Tooltip("Максимальная дальность стрельбы в метрах")]
    public float range = 100f;

    [Tooltip("Какие слои луч должен учитывать")]
    public LayerMask hitMask = ~0;

    [Tooltip("Префаб трассера")]
    public BulletTracer tracerPrefab;

    [Tooltip("Точка на персонаже, откуда визуально вылетает трассер")]
    public Transform muzzle;

    public void OnFire(InputValue value)
    {
        if (!value.isPressed) return;

        Shoot();
    }

    void Shoot()
    {
        if (cam == null)
        {
            Debug.LogWarning("Camera не назначена в Shooting!");
            return;
        }

        // RaycastHit хранит информацию о том, во что попал луч
        RaycastHit hit;

        // Куда долетит трассер
        Vector3 endPoint;

        // Пускаем луч из центра камеры вперёд (Physics.Raycast возвращает true,
        // если луч во что-то врезался в пределах range)
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, hitMask))
        {
            Debug.Log("Попал в: " + hit.collider.gameObject.name);
            endPoint = hit.point;

            // сюда добавить нанесение урона врагам народа
 
        }
        else
        {
            Debug.Log("Промах");
            endPoint = cam.transform.position + cam.transform.forward * range;
        }

        Vector3 tracerStart = muzzle != null ? muzzle.position : cam.transform.position;
        SpawnTracer(tracerStart, endPoint);
    }

    void SpawnTracer(Vector3 start, Vector3 end)
    {
        if (tracerPrefab == null) return;

        BulletTracer tracer = Instantiate(tracerPrefab, start, Quaternion.identity);
        tracer.Show(start, end);
    }

    void OnDrawGizmosSelected()
    {
        if (cam == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cam.transform.position, cam.transform.position + cam.transform.forward * range);
    }
}