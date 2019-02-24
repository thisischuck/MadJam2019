using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int dashSpeed;
    public int speed;
    public int jumpForce;
    public float fallMultipplier = 2.5f;
    public float LowJumpMultiplier = 2f;
    float distToGround;

    //Animator
    public Animator animator;
    public bool rightSide = true;

    public bool isMovable = false;
    //public bool isDashing = false;

    private KeyCode lastKey;
    private float timeToDash;


    public float keyTapTimeFrame = 0.5f;

    //----------------------------
    private int keyTapCount_right = 0;
    private float keyTapCool_right = 0f;

    private int keyTapCount_left = 0;
    private float keyTapCool_left = 0f;

    private int keyTapCount_forward = 0;
    private float keyTapCool_forward = 0f;

    private int keyTapCount_back = 0;
    private float keyTapCool_back = 0f;
    //------------------------------------

    public bool isDashing = false;
    private Vector3 dir = Vector3.zero;
    private float dashDurCount = 0;
    [Tooltip("Dash Duration.")]
    public float dashDur = 1;

    //----------------Dash Cooldown----------------------
    public bool hasDashed = false;
    [Tooltip("Dash cooldown.")]
    public float dashCooldown;
    private float dashCDRCount;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        distToGround = GetComponent<BoxCollider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovable)
        {
            Movement();
            Dash();
        }
    }

    private void Movement()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(PlayerSettings.Instance.Forward))
        {
            //rb.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
            move += Vector3.forward;// * Time.deltaTime;
            lastKey = PlayerSettings.Instance.Forward;
        }
        else if (Input.GetKey(PlayerSettings.Instance.Back))
        {
            //rb.MovePosition(transform.position + Vector3.back * speed * Time.deltaTime);
            move += Vector3.back;
            lastKey = PlayerSettings.Instance.Back;
        }

        if (Input.GetKey(PlayerSettings.Instance.Left))
        {
            //rb.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
            //rb.velocity += Vector3.left * speed * Time.deltaTime;
            move += Vector3.left;
            lastKey = PlayerSettings.Instance.Left;
        }
        else if (Input.GetKey(PlayerSettings.Instance.Right))
        {
            //rb.MovePosition( transform.position + Vector3.right * speed * Time.deltaTime);
            //rb.velocity += Vector3.right * speed * Time.deltaTime;
            move += Vector3.right;
            lastKey = PlayerSettings.Instance.Right;
        }

        if (Input.GetKeyDown(PlayerSettings.Instance.Jump))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        if (move != Vector3.zero || isDashing)
        {
            rb.velocity = move.normalized * speed * Time.deltaTime;
        }

        if (isDashing)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Rolling", true);
            animator.SetBool("Jumping", false);
        }
        else if (!IsGrounded())
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Rolling", false);
            animator.SetBool("Jumping", true);
        }
        else if (move != Vector3.zero && !isDashing)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", true);
            animator.SetBool("Rolling", false);
            animator.SetBool("Jumping", false);
        }
        else if (move == Vector3.zero && !isDashing)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walking", false);
            animator.SetBool("Rolling", false);
            animator.SetBool("Jumping", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(PlayerSettings.Instance.Jump))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastKey = PlayerSettings.Instance.Jump;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }

    private void Dash()
    {
        if (Input.GetKeyDown(PlayerSettings.Instance.Right) && !isDashing && !hasDashed) //Right Dash
        {
            if (keyTapCool_right > 0 && keyTapCount_right >= keyTapTimeFrame)
            {
                isDashing = true;
                hasDashed = true;

                dir = transform.right;
                dashDurCount = 0;
                dashCDRCount = 0;
            }
            else
            {
                keyTapCool_right += 0.5f;
                keyTapCount_right += 1;
            }
        }
        if (Input.GetKeyDown(PlayerSettings.Instance.Left) && !isDashing && !hasDashed) //Right Dash
        {
            if (keyTapCool_left > 0 && keyTapCount_left >= keyTapTimeFrame)
            {
                isDashing = true;
                hasDashed = true;

                dir = -transform.right;
                dashDurCount = 0;
                dashCDRCount = 0;
            }
            else
            {
                keyTapCool_left += 0.5f;
                keyTapCount_left += 1;
            }
        }
        if (Input.GetKeyDown(PlayerSettings.Instance.Back) && !isDashing && !hasDashed) //Right Dash
        {
            if (keyTapCool_back > 0 && keyTapCount_back >= keyTapTimeFrame)
            {
                isDashing = true;
                hasDashed = true;

                dir = -transform.forward;
                dashDurCount = 0;
                dashCDRCount = 0;
            }
            else
            {
                keyTapCool_back += 0.5f;
                keyTapCount_back += 1;
            }
        }
        if (Input.GetKeyDown(PlayerSettings.Instance.Forward) && !isDashing && !hasDashed) //Right Dash
        {
            if (keyTapCool_forward > 0 && keyTapCount_forward >= keyTapTimeFrame)
            {
                isDashing = true;
                hasDashed = true;

                dir = transform.forward;
                dashDurCount = 0;
                dashCDRCount = 0;
            }
            else
            {
                keyTapCool_forward += 0.5f;
                keyTapCount_forward += 1;
            }
        }
    }

    private void CharacterTappingControl()
    {
        //Right Tap
        if (keyTapCool_right > 0)
        {
            keyTapCool_right -= 1 * Time.deltaTime;
        }
        else
        {
            keyTapCount_right = 0;
        }

        if (keyTapCount_right > 2)
        {
            keyTapCount_right = 0;
        }

        //Left Tap
        if (keyTapCool_left > 0)
        {
            keyTapCool_left -= 1 * Time.deltaTime;
        }
        else
        {
            keyTapCount_left = 0;
        }

        if (keyTapCount_left > 2)
        {
            keyTapCount_left = 0;
        }

        //Back Tap
        if (keyTapCool_back > 0)
        {
            keyTapCool_back -= 1 * Time.deltaTime;
        }
        else
        {
            keyTapCount_back = 0;
        }

        if (keyTapCount_back > 2)
        {
            keyTapCount_back = 0;
        }

        //Forward Tap
        if (keyTapCool_forward > 0)
        {
            keyTapCool_forward -= 1 * Time.deltaTime;
        }
        else
        {
            keyTapCount_forward = 0;
        }

        if (keyTapCount_forward > 2)
        {
            keyTapCount_forward = 0;
        }
    }

    private void CharacterDashControl()
    {
        if (hasDashed)
        {
            if (isDashing)
            {
                dashDurCount += 1 * Time.deltaTime;
            }
            else
            {
                dashCDRCount += 1 * Time.deltaTime;
            }

            rb.AddForce(dir * dashSpeed, ForceMode.Impulse);
        }

        if (dashDurCount >= dashDur && isDashing) //End of the dash
        {
            dashDurCount = 0;
            isDashing = false;
            dir = Vector3.zero;
            animator.SetBool("Rolling", false);
        }

        if (dashCDRCount >= dashCooldown && hasDashed) //End of the cooldown
        {
            dashCDRCount = 0;
            hasDashed = false;
        }
    }

    private void FixedUpdate()
    {
        //BetterJumping
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultipplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
        }

        CharacterTappingControl();
        CharacterDashControl();
    }
}
