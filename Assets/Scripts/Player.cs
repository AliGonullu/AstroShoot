using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MovementJoystick joystick;
    [SerializeField] private GameObject engineEffect;
    [SerializeField] private SceneMNG scene_ref;

    public static int playerSpeedLVL = 0, health = 3;
    private float playerSpeed = 305, zAxis;
    private SpriteRenderer spriteRenderer;
    //private readonly BallSlot ballSlot;
    private bool kick = false;
    private Rigidbody2D rb;


    public bool GetKick(){return kick;}
    public void SetKick(bool value){kick = value;}

    private void Start()
    {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
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
                engineEffect.SetActive(true);
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
                engineEffect.SetActive(false);
                rb.freezeRotation = true;
                float friction = 3.0f;
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
        if (collision.gameObject.name.StartsWith("Obstacle"))
        {
            Destroy(collision.gameObject);
            health -= 1;
            switch (health)
            {
                case 3:
                    spriteRenderer.material.color = new Color(0, 0, 0); break;
                case 2:
                    spriteRenderer.material.color = new Color(1, 0.5f, 0.5f); break;
                case 1:
                    spriteRenderer.material.color = new Color(0.9f, 0.3f, 0.3f); break;
            }
            if(health <= 0)
                scene_ref.OpenGameOverMenu();
        }
    }

}
