using UnityEngine;

public class Player : MonoBehaviour
{
    private SoundEffect _soundEffect;

    [SerializeField] private ScreenShake screenShake;
    [SerializeField] private MovementJoystick joystick;
    [SerializeField] private GameObject[] engineEffects;
    [SerializeField] private SceneMNG scene_ref;
    [SerializeField] private Ball ball;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private readonly float playerSpeed = 305;
    private float zAxis = 0f;
    private bool kick = false;
    
    public bool GetKick() { return kick; }
    public void SetKick(bool value) { kick = value; }


    private void Start()
    {
        _soundEffect = GetComponentInChildren<SoundEffect>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        var _sprite = Resources.Load<Sprite>("Sprites/Ships/" + "Ship" + Variables.ship_no.ToString());
        spriteRenderer.sprite = _sprite;
        rb = GetComponent<Rigidbody2D>();

        for (int i = 0; i < engineEffects.Length; i++)
        {
            
            if(Variables.ship_engine_effect_idx != i)
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
                engineEffects[Variables.ship_engine_effect_idx].SetActive(true);
                rb.freezeRotation = false;
                float level_benefit = Mathf.Pow(Variables.ship_speed_lvl, 1.5f) * Mathf.Pow(Variables.ship_no, 1.5f);
                rb.velocity = new Vector2(joystick.GetJoystickVector2().x * (playerSpeed + level_benefit) * Time.deltaTime, joystick.GetJoystickVector2().y * (playerSpeed + level_benefit) * Time.deltaTime);
                float hAxis = joystick.GetJoystickVector2().x;
                float vAxis = joystick.GetJoystickVector2().y;
                zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -zAxis));
            }
            else
            {
                engineEffects[Variables.ship_engine_effect_idx].SetActive(false);
                rb.freezeRotation = true;
                float friction = 2.5f * (Variables.ship_no);
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
            _soundEffect.MetalSoundEffect();
            Variables.ship_health -= 1;
            switch (Variables.ship_health)
            {
                case 3:
                    spriteRenderer.material.color = new Color(0, 0, 0); break;
                case 2:
                    spriteRenderer.material.color = new Color(1, 0.5f, 0.5f); break;
                case 1:
                    spriteRenderer.material.color = new Color(0.9f, 0.3f, 0.3f); break;
            }
            if(Variables.ship_health <= 0)
                scene_ref.OpenGameOverMenu();
            else
                StartCoroutine(screenShake.Shake(0.3f, 0.45f));
        }
    }

}
