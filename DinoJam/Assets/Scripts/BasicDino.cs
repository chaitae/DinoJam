using UnityEngine;
using UnityEngine.Tilemaps;

public class BasicDino : MonoBehaviour
{
    private enum DinoState
    {
        Normal,
        Fire
    }

    /*
     * TODO:
     * 2) Create a tile where lava can only be started from
     */
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidbody;
    [Space]
    public float defaultMoveSpeed;
    public float fireMoveSpeed;
    [Space]
    public float minMoveTime;
    public float maxMoveTime;
    [Space]
    public float minWaitTime;
    public float maxWaitTime;
    [Space]
    public Color fireColor;
    [Space]
    public OnFireListener[] onFireListeners;

    private bool isMoving;
    private float moveSpeed;
    private Vector2 moveDirection;
    private float maxMoveTimer;
    private float moveTimer;
    private DinoState state;

    private Tilemap tilemap => LevelManager.instance.tilemap;

    public void Awake()
    {
        state = DinoState.Normal;
        isMoving = false;
        CalculateMovement();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        BasicDino otherDino = other.GetComponent<BasicDino>();
        if (otherDino == null || state == DinoState.Normal)
            return;
        otherDino.SetOnFire();
    }

    public void FixedUpdate()
    {
        moveTimer += Time.fixedDeltaTime;
        if(moveTimer > maxMoveTimer)
            CalculateMovement();
        LavaTile lavaTile = tilemap.GetTile<LavaTile>(new Vector3Int(Mathf.FloorToInt(transform.position.x),
            Mathf.FloorToInt(transform.position.y),
            0));
        if(lavaTile != null)
            SetOnFire();
        CheckPosition();
    }

    public void SetOnFire()
    {
        if(state == DinoState.Fire)
            return;
        state = DinoState.Fire;
        spriteRenderer.color = fireColor;
        foreach(OnFireListener onFireListener in onFireListeners)
        {
            onFireListener.SetOnFire();
        }
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        isMoving = !isMoving;
        moveTimer = 0;
        if(state == DinoState.Fire)
        {
            moveSpeed = fireMoveSpeed;
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            rigidbody.velocity = moveDirection * moveSpeed;
            maxMoveTimer = Random.Range(minMoveTime, maxMoveTime);
            return;
        }

        if(isMoving)
        {
            moveSpeed = defaultMoveSpeed;
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            rigidbody.velocity = moveDirection * moveSpeed;
            maxMoveTimer = Random.Range(minMoveTime, maxMoveTime);
        }
        else
        {
            moveSpeed = 0;
            maxMoveTimer = Random.Range(minWaitTime, maxWaitTime);
            rigidbody.velocity = moveDirection * moveSpeed;
        }
    }

    private void CheckPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = 0;
        if (LevelManager.instance.levelBounds.Contains(newPosition))
            return;
        newPosition = LevelManager.instance.levelBounds.ClosestPoint(newPosition);
        SetPosition(newPosition);
    }

    private void SetPosition(Vector2 newPosition)
    {
        Vector3 position = transform.position;
        position.x = newPosition.x;
        position.y = newPosition.y;
        transform.position = position;
    }
}
