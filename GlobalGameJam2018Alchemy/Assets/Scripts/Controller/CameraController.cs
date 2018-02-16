using UnityEngine;

[RequireComponent(typeof(GameController))]
public class CameraController : MonoBehaviour
{
    private const int CameraMouseButton = 2;
    public Camera controlledCamera;

    private Vector3 origin;
    private bool active = false;

    private Vector3 target;
    private bool tracking = false;

    private void Start()
    {
        origin = transform.position;
        var game = GetComponent<GameController>();
        game.GameOver += _ => active = false;
        game.LevelStarted += () =>
        {
            transform.position = origin;
            active = true;
        };
    }

    private void LateUpdate()
    {
        if (!active) { return; }
        if (Input.GetMouseButtonDown(CameraMouseButton))
        {
            target = IntersectGround();
            tracking = true;
        }
        if (Input.GetMouseButtonUp(CameraMouseButton)) { tracking = false; }
        if (tracking)
        {
            var newTarget = IntersectGround();
            controlledCamera.transform.position += (target - newTarget);
        }
    }

    private Vector3 IntersectGround()
    {
        var ray = controlledCamera.ScreenPointToRay(Input.mousePosition);
        var t = (0f - ray.origin.y) / ray.direction.y; // Intersect xz-plane
        return ray.origin + t * ray.direction;
    }
}
