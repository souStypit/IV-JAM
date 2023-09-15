using UnityEngine;

public class EarthBehavior : MonoBehaviour
{
    [SerializeField] private float maxZoom, minZoom;
    [SerializeField] float rotateScale;
    [SerializeField] float spawnCooldown;
    [SerializeField] GameObject encounter;

    private bool isRotating = false;
    private Vector3 previousMousePosition;
    private float spawnCounter;

    private void Start()
    {
        spawnCounter = spawnCooldown;
    }

    private void Update()
    {
        spawnCounter -= Time.deltaTime;

        Zoom();
        Drag();
        SpawnEncounters();
    }

    private void SpawnEncounters()
    {
        if (spawnCounter > 0) return;

        Vector3 randomPoint = Random.onUnitSphere * 20.2f;
        Instantiate(encounter, randomPoint, Quaternion.identity, gameObject.transform);
        spawnCounter = spawnCooldown;
    }

    

    private void Drag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isRotating = true;
                    previousMousePosition = Input.mousePosition;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = (currentMousePosition - previousMousePosition).normalized * rotateScale;

            transform.Rotate(Vector3.right, mouseDelta.y, Space.World);
            transform.Rotate(Vector3.up, -mouseDelta.x, Space.World);

            previousMousePosition = currentMousePosition;
        }
    }

    private void Zoom()
    {
        if (Input.mouseScrollDelta.y == 0) return;
        
        float newZ = Input.mouseScrollDelta.y + Camera.main.transform.position.z;
        Camera.main.transform.position = new Vector3(0, 0, Mathf.Clamp(newZ, minZoom, maxZoom));
    }
}
