using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TwoPlayersOneKeyboard : MonoBehaviour
{
    [Header("Actions(drag from your Input Actions Asset)")]
    [SerializeField] private InputActionReference p1Move;
    [SerializeField] private InputActionReference p2Move;

    [SerializeField] private InputActionReference p1Jump;
    [SerializeField] private InputActionReference p2Jump;

    [Header("Players")]
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;

    [SerializeField] private float speed = 5f;

    [SerializeField] private int jumpForce = 5;

    private Rigidbody rb1;
    private Rigidbody rb2;


    private void Start()
    {
        rb1 = p1.GetComponent<Rigidbody>();
        rb2 = p2.GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        p1Move.action.Enable();
        p2Move.action.Enable();

        p1Jump.action.Enable();
        p2Jump.action.Enable();
    }

    private void OnDisable()
    {
        p1Move.action.Disable();
        p2Move.action.Disable();

        p1Jump.action.Disable();
        p2Jump.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var m1 = p1Move.action.ReadValue<Vector2>();
        var m2 = p2Move.action.ReadValue<Vector2>();

        if (p1) p1.position += new Vector3(m1.x, 0f, m1.y) * Time.deltaTime;
        if (p2) p2.position += new Vector3(m2.x, 0f, m2.y) * Time.deltaTime;


       var j1 = p1Jump.action.ReadValue<float>();

        if (j1 > 0f)
        {
            p1Jumps();
        }

        var j2 = p2Jump.action.ReadValue<float>();

        if (j2 > 0f)
        {
            p2Jumps();
        }
    }

    public void p1Jumps()
    {
        rb1.linearVelocity = new Vector3(rb1.linearVelocity.x, jumpForce, rb1.linearVelocity.z);
    }

    public void p2Jumps()
    {
        rb2.linearVelocity = new Vector3(rb2.linearVelocity.x, jumpForce, rb2.linearVelocity.z);
    }

    

}
