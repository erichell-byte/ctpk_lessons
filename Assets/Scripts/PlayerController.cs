using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movSpeed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;

    private readonly string VerticalAxis = "Vertical";
    private readonly string HorizontalAxis = "Horizontal";

    private Rigidbody _rb;
    private bool doubleJump;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (Input.GetAxis(VerticalAxis) * movSpeed * Time.deltaTime));
        transform.Rotate(Vector3.up * (Input.GetAxis(HorizontalAxis) * rotSpeed * Time.deltaTime));

        if (IsGrounded())
        {
            doubleJump = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded() || doubleJump)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, jumpingPower, _rb.velocity.z);

                doubleJump = !doubleJump;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        var colliders = Physics.OverlapSphere(groundChecker.position, 0.2f, groundLayer);
        return colliders.Length > 0;
    }
}
