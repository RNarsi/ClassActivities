using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;


[RequireComponent (typeof(CharacterController))]
public class NetworkFPSPlayer : NetworkBehaviour 
{
    [Header("Player Components")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Animator animator;

    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float maxPitch = 80f;

    [Header("Animator Params")]
    [SerializeField] private string speedParam = "Speed";
    
    private PlayerInput pi;
    private InputAction lookAction;
    [SerializeField]
    private InputAction moveAction;
    private CharacterController cc;

    private float pitch;

    public override void OnNetworkSpawn()
    {
       cc = GetComponent<CharacterController>();
       pi = GetComponent<PlayerInput>();

        if (!IsOwner)
        {
            if (playerCamera) playerCamera.enabled = false;
            if(pi) pi.enabled = false;
            return;
        }

        moveAction = pi.actions["Move"];
        lookAction = pi.actions["Look"];
        moveAction.Enable();
        lookAction.Enable();

        if(playerCamera) playerCamera.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 m = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * m.x + transform.forward * m.y;
        cc.Move(move * moveSpeed * Time.deltaTime);

        Vector2 look = lookAction.ReadValue<Vector2>() * lookSensitivity;
        transform.Rotate(0f, look.x, 0f);

        pitch -= look.y;
        pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);
        cameraPivot.localEulerAngles = new Vector3 (pitch, 0f, 0f);

        if (animator) animator.SetFloat(speedParam, m.magnitude);

    }
}
