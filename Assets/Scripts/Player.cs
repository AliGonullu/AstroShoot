using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ScreenShake screenShake;
    [SerializeField] private MovementJoystick joystick;
    [SerializeField] private GameObject[] engineEffects;
    [SerializeField] private SceneMNG scene_ref;
    public static int engine_effect_idx = 0, first_health = 3, playerSpeedLVL = 1;
    private static int health = first_health, ship_no = 1;
    private readonly float playerSpeed = 305;
    private float zAxis = 0f;
    private SpriteRenderer spriteRenderer;
    private bool kick = false;
    private Rigidbody2D rb;

    public void SetHealth(int value) { health = value; }

    public int GetShipNo() { return ship_no; }
    public void SetShipNo(int _no) { ship_no = _no; }

    public bool GetKick() { return kick; }
    public void SetKick(bool value) { kick = value; }

    

    private void Start()
    {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        var _sprite = Resources.Load<Sprite>("Sprites/Ships/" + "Ship" + ship_no.ToString());
        spriteRenderer.sprite = _sprite;
        rb = GetComponent<Rigidbody2D>();

        for (int i = 0; i < engineEffects.Length; i++)
        {
            if(engine_effect_idx != i)
                Destroy(engineEffects[i]);
        }
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
                engineEffects[engine_effect_idx].SetActive(true);
                rb.freezeRotation = false;
                float level_benefit = Mathf.Pow(playerSpeedLVL, 2) * Mathf.Pow(ship_no, 3f);
                rb.velocity = new Vector2(joystick.GetJoystickVector2().x * (playerSpeed + level_benefit) * Time.deltaTime, joystick.GetJoystickVector2().y * (playerSpeed + level_benefit) * Time.deltaTime);
                float hAxis = joystick.GetJoystickVector2().x;
                float vAxis = joystick.GetJoystickVector2().y;
                zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -zAxis));
            }
            else
            {
                engineEffects[engine_effect_idx].SetActive(false);
                rb.freezeRotation = true;
                float friction = 2.5f * (ship_no);
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
            else
                StartCoroutine(screenShake.Shake(0.3f, 0.45f));
        }
    }
}
