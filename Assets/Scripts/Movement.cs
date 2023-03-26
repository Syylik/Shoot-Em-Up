using UnityEngine;

public class Movement : MonoBehaviour 
{
    public float moveSpeed;
    internal Vector2 _moveDirection;
    [SerializeField] private bool _isClamped = true;  //убирать на пулях

    public void Init(Vector2 moveDir) => _moveDirection = moveDir;

    public void Init(float speed, Vector2 moveDir)
    {
        moveSpeed = speed;
        _moveDirection = moveDir;
    }


    private void Update() => Move(_moveDirection);

    public void Move(Vector2 input)
    {
        Vector2 pos = transform.position;
        Vector2 moveAmount = input * moveSpeed * Time.deltaTime;

        pos += moveAmount;

        if(_isClamped) pos = new Vector2(Mathf.Clamp(pos.x, -8.25f, 8.5f), Mathf.Clamp(pos.y, -4.5f, 4.5f));
        transform.position = pos;
    }
}
