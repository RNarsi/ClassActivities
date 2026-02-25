
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOneScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpStep = 1f;
    [SerializeField] private float rotateSpeed = 100f;

    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 12f;


    private Vector2 lookInput;
    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        float y = lookInput.x * rotateSpeed * Time.deltaTime;
        transform.Rotate(0f, y, 0f, Space.World);

        Vector3 move3 = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;
        transform.position += move3;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
       lookInput = context.ReadValue<Vector2>();

    }

    public void OnJump (InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        transform.position += Vector3.up * jumpStep;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        proj.GetComponent<Rigidbody>().linearVelocity = firePoint.forward * projectileSpeed;

    }
}
