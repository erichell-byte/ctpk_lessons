using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movSpeed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Detector detector;
    [SerializeField] private Animator animator;

    

    private Rigidbody _rb;
    // for test
    public bool _doubleJump;
    
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _movement;
    private float _currentRotate;
    private float _rotVel;
    private bool nowIsJumping;

    public Action OnBuildCollected;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        detector.OnBuildDetected += BuildTouched;
    }

    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        detector.OnBuildDetected -= BuildTouched;
    }

    void Update()
    {
        if (IsGrounded() && _rb.velocity.y < 0 && !nowIsJumping)
        {
            _doubleJump = false;
        }
    }
    
    private void FixedUpdate()
    {
        _movement = new Vector3(0, 0, _verticalInput).normalized;
        _rb.AddRelativeForce(_movement * movSpeed, ForceMode.VelocityChange);
        
        float rotateY = _horizontalInput * rotSpeed;
        _rb.AddRelativeTorque(Vector3.up * rotateY, ForceMode.VelocityChange);
        
        if (_verticalInput == 0)
            animator.SetBool("IsRun", false);
    }
    
    private void BuildTouched(Collider build)
    {
        Destroy(build.gameObject);
        OnBuildCollected?.Invoke();
    }

    private bool IsGrounded()
    {
        var colliders = Physics.OverlapSphere(groundChecker.position, 0.1f, groundLayer);
        return colliders.Length > 0;
    }

    public void OnMovePressed(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        _horizontalInput = move.x;
        _verticalInput = move.y;
        animator.SetBool("IsRun", true);
        Debug.Log("StartMove");
    }

    public void OnJumpPressed(InputAction.CallbackContext context)
    {
        if ((IsGrounded() || _doubleJump) && context.performed)
        {
            _rb.AddForce(Vector3.up * jumpingPower, ForceMode.Impulse);
            _doubleJump = !_doubleJump;
            nowIsJumping = true;
            StartCoroutine(DelayToCheckGround());
            animator.SetBool("IsRun", true);
        }
    }

    private IEnumerator DelayToCheckGround()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        nowIsJumping = false;
    }
}
