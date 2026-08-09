using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class BulletTracer : MonoBehaviour
{
    [Tooltip("Сколько секунд трассер будет виден перед тем как исчезнуть")]
    public float duration = 0.05f;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Вызывается из Shooting, чтобы задать начало и конец линии
    public void Show(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}