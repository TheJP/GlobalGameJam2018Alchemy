using UnityEngine;

[RequireComponent(typeof(GameController))]
public class CameraController : MonoBehaviour
{
    private const int CameraMouseButton = 2;

    public Camera controlledCamera;

    [Tooltip("Maximal allowed distance from original position in any direction")]
    public float maxCameraMovement = 50f;

    [Tooltip("Time in seconds between two clicks, that counts them as double click")]
    public float doubleClickThreshold = 1f;

    [Tooltip("Move the camera while the mouse is this distance from the edge of the window")]
    public float moveWhileDistanceFromEdge = 10f;

    [Tooltip("Speed of the camera movement when cursor is at the edge of the screen")]
    public float cameraMovementSpeed = 1f;

    [Header("Zoom")]
    [Tooltip("Speed with which the camera is zoomed in or out")]
    public float zoomSpeed = 5f;

    [Tooltip("Minimal distance of the camera from the ground (xz-plane)")]
    public float minZoomDistance = 0.2f;

    [Tooltip("Maximal distance of the camera from the ground (xz-plane)")]
    public float maxZoomDistance = 10f;

    private Vector3 origin;
    private bool active = false;

    private Vector3 target;
    private bool tracking = false;
    private float lastClickTime = 0f;

    private void Start()
    {
        origin = controlledCamera.transform.position;
        var game = GetComponent<GameController>();
        game.GameOver += _ =>
        {
            Cursor.lockState = CursorLockMode.None;
            active = false;
        };
        game.LevelStarted += () =>
        {
            Cursor.lockState = CursorLockMode.Confined;
            controlledCamera.transform.position = origin;
            active = true;
        };
    }

    private void LateUpdate()
    {
        if (!active) { return; }
        if (Input.GetMouseButtonDown(CameraMouseButton))
        {
            if (Time.time - lastClickTime <= doubleClickThreshold)
            {
                // Double clicked: Reset camera
                controlledCamera.transform.position = origin;
            }
            else
            {
                // Start camera movement
                target = IntersectGround();
                tracking = true;
                lastClickTime = Time.time;
            }
        }
        // Stop camera movement
        if (Input.GetMouseButtonUp(CameraMouseButton)) { tracking = false; }
        if (tracking)
        {
            // Move camera
            var newTarget = IntersectGround();
            controlledCamera.transform.position += (target - newTarget);
        }

        // Move the camera while the cursor is at the edge of the screen
        // Don't move while in development evironment, beacuse it's annoying
        // if the camera moves when one wants to use the Scene View or Inspector
        if (!tracking && !Debug.isDebugBuild)
        {
            var direction = Vector3.zero;
            if(Input.mousePosition.x <= moveWhileDistanceFromEdge) { direction.x = -1; }
            if (Input.mousePosition.y <= moveWhileDistanceFromEdge) { direction.z = -1; }
            if (Input.mousePosition.x >= Screen.width - moveWhileDistanceFromEdge) { direction.x = 1; }
            if (Input.mousePosition.y >= Screen.height - moveWhileDistanceFromEdge) { direction.z = 1; }
            controlledCamera.transform.position += direction.normalized * cameraMovementSpeed;
        }

        // Zoom in and out
        var scrolling = -Input.GetAxis("Mouse ScrollWheel");
        controlledCamera.transform.position += Vector3.up * scrolling * zoomSpeed;

        // Restrict camera movement
        var position = controlledCamera.transform.position;
        position.x = Mathf.Clamp(position.x, origin.x - maxCameraMovement, origin.x + maxCameraMovement);
        position.y = Mathf.Clamp(position.y, minZoomDistance, maxZoomDistance);
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
