using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class StartCubeMoveAnim : NetworkBehaviour
{
    private NetworkAnimator netAnim;

    public override void OnNetworkSpawn()
    {
        netAnim = GetComponent<NetworkAnimator>();

        if (IsServer)
        {
            netAnim.Animator.SetTrigger("StartRotate");
        }
    }

}
