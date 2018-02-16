using UnityEngine;

[RequireComponent(typeof(GameController))]
public class CameraController : MonoBehaviour
{
    private const int CameraMouseButton = 2;

    public Camera controlledCamera;

    [Tooltip("Maximal allowed distance from original position in any direction")]
    public float maxCameraMovement = 50f;

    private Vector3 origin;
    private bool active = false;

    private Vector3 target;
    private bool tracking = false;

    private void Start()
    {
        origin = transform.position;
        var game = GetComponent<GameController>();
        game.GameOver += _ =>
        {
            Cursor.lockState = CursorLockMode.None;
            active = false;
        };
        game.LevelStarted += () =>
        {
            Cursor.lockState = CursorLockMode.Confined;
            transform.position = origin;
            active = true;
        };
    }

    private void LateUpdate()
    {
        if (!active) { return; }
        if (Input.GetMouseButtonDown(CameraMouseButton))
        {
            // Start camera movement
            target = IntersectGround();
            tracking = true;
        }
        // Stop camera movement
        if (Input.GetMouseButtonUp(CameraMouseButton)) { tracking = false; }
        if (tracking)
        {
            // Move camera
            var newTarget = IntersectGround();
            controlledCamera.transform.position += (target - newTarget);
        }

        // Restrict camera movement
        var position = controlledCamera.transform.position;
        position.x = Mathf.Clamp(position.x, origin.x - maxCameraMovement, origin.x + maxCameraMovement);
        position.z = Mathf.Clamp(position.z, origin.z - maxCameraMovement, origin.z + maxCameraMovement);
        controlledCamera.transform.position = position;
    }

    /// <summary>Finds the point on the xz-plane that the pointer is hovering over.</summary>
    private Vector3 IntersectGround()
    {
        var ray = controlledCamera.ScreenPointToRay(Input.mousePosition);
        var t = (0f - ray.origin.y) / ray.direction.y; // Intersect xz-plane
        return ray.origin + t * ray.direction;
    }
}
