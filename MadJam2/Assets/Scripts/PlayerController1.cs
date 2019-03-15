using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public int dashSpeed;
    public int speed;
    public int jumpForce;
    public float fallMultipplier = 2.5f;
    public float LowJumpMultiplier = 2f;
    float distToGround;

    private SpriteRenderer sprite;

    //Animator
    public Animator animator;
    public bool rightSide = true;

    public bool isMovable = false;

    public float keyTapTimeFrame = 0.5f;

    public bool isDashing = false;
    private Vector3 dir = Vector3.zero;
    private float dashDurCount = 0;
    [Tooltip("Dash Duration.")]
    public float dashDur = 1;

    //----------------Dash Cooldown----------------------
    public bool isDashable = true;
    [Tooltip("Dash cooldown.")]
    public float dashCooldown;
    private float dashCDRCount;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
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
        }
        else if (Input.GetKey(PlayerSettings.Instance.Back))
        {
            //rb.MovePosition(transform.position + Vector3.back * speed * Time.deltaTime);
            move += Vector3.back;
        }

        if (Input.GetKey(PlayerSettings.Instance.Left))
        {
            //rb.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
            //rb.velocity += Vector3.left * speed * Time.deltaTime;
            move += Vector3.left;
            sprite.flipX = true;
        }
        else if (Input.GetKey(PlayerSettings.Instance.Right))
        {
            sprite.flipX = false;
            //rb.MovePosition( transform.position + Vector3.right * speed * Time.deltaTime);
            //rb.velocity += Vector3.right * speed * Time.deltaTime;
            move += Vector3.right;
        }

        if (Input.GetKeyDown(PlayerSettings.Instance.Jump) && IsGrounded())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        if (move != Vector3.zero || isDashing)
        {
            rb.velocity = new Vector3(move.normalized.x * speed * Time.deltaTime, rb.velocity.y, move.normalized.z * speed * Time.deltaTime) ;
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
        if (IsGrounded())
        {
            if (Input.GetKeyDown(PlayerSettings.Instance.Jump) && IsGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }

    private void Dash()
    {
        if (Input.GetKey(PlayerSettings.Instance.Right) && !isDashing ) //Right Dash
        {
			if (Input.GetKeyDown(KeyCode.E) && isDashable)
            {
                isDashing = true;
				isDashable = false;

                dir = transform.right;
                dashDurCount = 0;
                dashCDRCount = 0;
				keyTapTimeFrame = Time.time+1f;
			}
        }
        if (Input.GetKeyDown(PlayerSettings.Instance.Left) && !isDashing) //Right Dash
        {
			if (Input.GetKeyDown(KeyCode.E) && isDashable)
			{
                isDashing = true;
				isDashable = false;

                dir = -transform.right;
                dashDurCount = 0;
                dashCDRCount = 0;
				keyTapTimeFrame = Time.time +1f;
			}
        }
        if (Input.GetKeyDown(PlayerSettings.Instance.Back) && !isDashing) //Right Dash
        {
			if (Input.GetKeyDown(KeyCode.E) && isDashable)
			{
                isDashing = true;
				isDashable = false;

				dir = -transform.forward;
                dashDurCount = 0;
                dashCDRCount = 0;
				keyTapTimeFrame = Time.time +1f;
			}
        }
        if (Input.GetKeyDown(PlayerSettings.Instance.Forward) && !isDashing) //Right Dash
        {
			if (Input.GetKeyDown(KeyCode.E) && isDashable)
			{
                isDashing = true;
				isDashable = false;

				dir = transform.forward;
                dashDurCount = 0;
                dashCDRCount = 0;
				keyTapTimeFrame = Time.time + 1f;
			}
        }
    }

    private void CharacterDashControl()
    {
        if (!isDashable)
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

        if (dashCDRCount >= dashCooldown && !isDashable) //End of the cooldown
        {
            dashCDRCount = 0;
			isDashable = true;
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

		CharacterDashControl();
    }
}
