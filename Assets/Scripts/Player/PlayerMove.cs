using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMove : MonoBehaviour
{
    private Vector2 movement;  // �÷��̾ �����̴� ����
    private float horizontal;  // �Է¹��� ���� ������ ����
    private Player player;     // �÷��̾� ������ �޾ƿ��� ���� ��ü

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (player.State != PlayerState.Die)
            horizontal = Input.GetAxisRaw("Horizontal");
        else
            horizontal = 0;

        // �÷��̾� ���� ��ȯ
        MoveStateSelector();
        // �÷��̾� ���� ��ȯ
        DirectionSelector();

        if (player.State != PlayerState.Dash)
        {
            // ������
            Movement();
        }
    }

    /// <summary>
    /// �÷��̾��� �̵����� ��ȯ�� �Ǵ��ϴ� �Լ��Դϴ�.
    /// </summary>
    private void MoveStateSelector()
    {
        if (!Input.anyKey && player.State == PlayerState.Run)
            player.State = PlayerState.Idle;

        else if (horizontal != 0 && player.State == PlayerState.Idle)
        {
            player.State = PlayerState.Run;
        }
    }

    /// <summary>
    /// �÷��̾ �����ִ� ������ �Ǵ��ϴ� �Լ��Դϴ�.
    /// </summary>
    private void DirectionSelector()
    {
        if (horizontal > 0)
        {
            player.Direction = CharacterDirection.Right;
            player.SpriteRender.flipX = false;
        }
        else if (horizontal < 0)
        {
            player.Direction = CharacterDirection.Left;
            player.SpriteRender.flipX = true;
        }
    }

    /// <summary>
    /// �������� ������ �Լ��Դϴ�.
    /// </summary>
    private void Movement()
    {
        movement = new Vector2(horizontal, 0f);
        movement.Normalize();
        player.Rigidbody.velocity = new Vector2(movement.x * player.Status.Speed, player.Rigidbody.velocity.y);
    }
}
