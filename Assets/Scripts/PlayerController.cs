using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movSpeed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float  rotSmoothTime= 0.5f;
    [SerializeField] private Detector detector;

    private readonly string VerticalAxis = "Vertical";
    private readonly string HorizontalAxis = "Horizontal";

    private Rigidbody _rb;
    private bool _doubleJump;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _movement;
    private float _currentRotate;
    private float _rotVel;

    public Action OnBuildCollected;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        detector.OnBuildDetected += BuildTouched;
    }

    private void OnDestroy()
    {
        detector.OnBuildDetected -= BuildTouched;
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis(HorizontalAxis);
        _verticalInput = Input.GetAxis(VerticalAxis);
        
        if (IsGrounded())
        {
            _doubleJump = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded() || _doubleJump)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, jumpingPower, _rb.velocity.z);

                _doubleJump = !_doubleJump;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }
    
    private void FixedUpdate()
    {
        _movement = new Vector3(0, 0, _verticalInput).normalized;
        _rb.AddRelativeForce(_movement * (movSpeed * Time.fixedDeltaTime), ForceMode.VelocityChange);
        
        float rotateY = _horizontalInput * rotSpeed * Time.fixedDeltaTime;
        _currentRotate = Mathf.SmoothDampAngle(_currentRotate, rotateY, ref _rotVel, rotSmoothTime * Time.fixedDeltaTime);
        transform.eulerAngles += new Vector3(0, _currentRotate, 0);
    }
    
    private void BuildTouched(Collider build)
    {
        Destroy(build.gameObject);
        OnBuildCollected?.Invoke();

    }

    private bool IsGrounded()
    {
        var colliders = Physics.OverlapSphere(groundChecker.position, 0.2f, groundLayer);
        return colliders.Length > 0;
    }
}
