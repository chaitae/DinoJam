using UnityEngine;
using UnityEngine.Tilemaps;

public class BasicDino : MonoBehaviour
{
    private enum DinoState
    {
        Alive,
        Fire,
        Dead
    }

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
    public OnDeathListener[] onDeathListeners;

    private bool isMoving;
    private float moveSpeed;
    private Vector2 moveDirection;
    private float maxMoveTimer;
    private float moveTimer;
    private DinoState state;

    private Tilemap tilemap => LevelManager.Instance.tilemap;

    public void Awake()
    {
        state = DinoState.Alive;
        isMoving = false;
        CalculateMovement();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        BasicDino otherDino = other.GetComponent<BasicDino>();
        if (otherDino == null || state != DinoState.Fire)
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

    public void Kill()
    {
        if (state == DinoState.Dead)
            return;
        state = DinoState.Dead;
        CalculateMovement();
        foreach(OnDeathListener onDeathListener in onDeathListeners)
        {
            onDeathListener.Killed();
        }
    }

    public void SetOnFire()
    {
        if(state == DinoState.Fire || state == DinoState.Dead)
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
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        switch(state)
        {
            case DinoState.Fire:
                moveSpeed = fireMoveSpeed;
                maxMoveTimer = Random.Range(minMoveTime, maxMoveTime);
                break;
            case DinoState.Alive:
                if(isMoving)
                {
                    moveSpeed = defaultMoveSpeed;
                    maxMoveTimer = Random.Range(minMoveTime, maxMoveTime);
                }
                else
                {
                    moveSpeed = 0;
                    maxMoveTimer = Random.Range(minWaitTime, maxWaitTime);
                }
                break;
            case DinoState.Dead:
                moveSpeed = 0;
                maxMoveTimer = float.MaxValue;
                break;

        }
        rigidbody.velocity = moveDirection * moveSpeed;
    }

    private void CheckPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = 0;
        if (LevelManager.Instance.LevelBounds.Contains(newPosition))
            return;
        newPosition = LevelManager.Instance.LevelBounds.ClosestPoint(newPosition);
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
