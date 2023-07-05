using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MovementJoystick joystick;
    [SerializeField] private SceneMNG scene_ref;
    public static int playerSpeedLVL = 0;
    private float playerSpeed = 305, zAxis;
    private Rigidbody2D rb;
    private bool kick = false;
    private readonly BallSlot ballSlot;

    public BallSlot GetBallSlot(){return ballSlot;}

    public bool GetKick(){return kick;}

    public void SetKick(bool value){kick = value;}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movement();
        if (transform.position.y < -10)
        {
            scene_ref.OpenGameOverMenu();
        }
    }

    private void Movement()
    {
        if (rb != null)
        {
            if (joystick.GetJoystickVector2() != Vector2.zero)
            {
                rb.freezeRotation = false;
                float level_benefit = ((playerSpeedLVL - 1) * 11);
                rb.velocity = new Vector2(joystick.GetJoystickVector2().x * (playerSpeed + level_benefit) * Time.deltaTime, joystick.GetJoystickVector2().y * (playerSpeed + level_benefit) * Time.deltaTime);
                float hAxis = joystick.GetJoystickVector2().x;
                float vAxis = joystick.GetJoystickVector2().y;
                zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -zAxis));
            }
            else
            {
                rb.freezeRotation = true;
                float friction = 4;
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, friction * Time.deltaTime);
            }
        }
    }

    public void ThrowClick()
    {
        kick = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("FireWall"))
        {
            scene_ref.OpenGameOverMenu();
        }
    }

}
