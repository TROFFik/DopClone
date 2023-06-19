using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _collider = null;
    [SerializeField] private Drawing _drawing = null;

    void Awake()
    {
        _drawing.changingSpriteAction += ResetColider;
    }

    private void ResetColider()
    {
        Destroy(_collider);
        _collider = gameObject.AddComponent<PolygonCollider2D>();
    }
}
