using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private Drawing _drawing = null;

    private bool _has—hanges = false;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            InputCalculations();

            if (Input.GetTouch(0).phase == TouchPhase.Ended && _has—hanges)
            {
                _drawing.SpriteChangeEnd();
            }
        }
    }

    private void InputCalculations()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);

        if (hit.collider != null)
        {
            if (_drawing.gameObject == hit.collider.gameObject)
            {
                _drawing.Draw(hit.point);

                _has—hanges = true;
            }
        }
    }
}
