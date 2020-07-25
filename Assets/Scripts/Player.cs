using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField]
    float speed = 0;

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    bool canMove = true;

    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }

    bool isGameOver = false;

    public bool IsGameOver
    {
        get
        {
            return isGameOver;
        }
        set
        {
            isGameOver = value;
        }
    }

    bool isInvincible = false;

    public bool IsInvincible
    {
        get
        {
            return isInvincible;
        }
        set
        {
            isInvincible = value;
        }
    }

    [SerializeField]
    float secondsInvincible = 0;

    [SerializeField]
    AudioSource audioSource = null;

    [SerializeField]
    AudioClip clip = null;

    [SerializeField]
    AudioClip goodClipInv = null;

    [SerializeField]
    AudioClip badClipInv = null;

    [SerializeField]
    int lightAmount = 0;

    public int LightAmount
    {
        get
        {
            return lightAmount;
        }
        set
        {
            lightAmount = value;
        }
    }

    public NullEvent OnPlayerWinsGame = new NullEvent();

    public FloatEvent OnDamagePlayer = new FloatEvent();

    public BoolEvent OnMakePlayerInvincible = new BoolEvent();

    public PlayerEvent OnAttemptLightCast = new PlayerEvent();

    // Start is called before the first frame update

    private float initialSpeed = 0.0f;
    void Start()
    {
        initialSpeed = speed;
        OnPlayerWinsGame.AddListener(() => IsGameOver = true);
        OnDamagePlayer.AddListener((seconds) =>
        {
            if (!IsInvincible)
            {
                audioSource.PlayOneShot(clip);
                StartCoroutine(BeInvincible());
                StartCoroutine(BeStunned(seconds));
            }
        });
        OnMakePlayerInvincible.AddListener((isGood) =>
        {
            if (!IsInvincible)
            {
                audioSource.PlayOneShot(isGood ? goodClipInv : badClipInv);
                StartCoroutine(BeInvincible());
            }
        });
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("CastLight"))
        {
            OnAttemptLightCast.Invoke(this);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (CanMove && !IsGameOver)
        {
            Vector2 movement = new Vector2();
            movement.x = speed * Time.fixedDeltaTime * Input.GetAxis("Horizontal");
            movement.y = speed * Time.fixedDeltaTime * Input.GetAxis("Vertical");
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            rb2d.MovePosition(pos + movement);
        }
    }

    IEnumerator BeInvincible()
    {
        IsInvincible = true;
        float elapsed = 0;
        while (elapsed < secondsInvincible)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        IsInvincible = false;
    }

    IEnumerator BeStunned(float seconds)
    {
        CanMove = false;
        float elapsed = 0;
        while (elapsed < seconds)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        CanMove = true;
    }

    public void ReturnToNormalSpeed()
    {
        speed = initialSpeed;
    }
}
