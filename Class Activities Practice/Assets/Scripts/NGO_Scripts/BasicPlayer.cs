using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicPlayer : NetworkBehaviour
{
    private float speed = 5f;
    private PlayerInput playerInput;
    private InputAction moveAction;

    public override void OnNetworkSpawn()
    {
        if(!IsOwner) return;

        //Get player input on THIS spawned player object
        playerInput = GetComponent<PlayerInput>();

        //Grab the action by name from contorls action asset
        moveAction = playerInput.actions["Move"];

        //enable the map/actions for the local owner
        playerInput.enabled = true;
        moveAction.Enable();
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        moveAction?.Disable();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsOwner || !IsSpawned) return;

        Vector2 move = moveAction.ReadValue<Vector2>();
        Vector3 move3 = new Vector3(move.x, 0f, move.y) * speed * Time.deltaTime;
        transform.position += move3;
    }
}
