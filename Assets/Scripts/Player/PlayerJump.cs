using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Player player;         // �÷��̾� ������ �޾ƿ��� ���� ��ü
    private Collider2D isGrounded;       // ���� ���� ����� Ȯ���ϴ� �뵵
    private PlatformEffector2D getGround;
    private int jumpCnt = 2;
    private Vector2 groundCheck;   // ���� üũ�ϱ� ���� ������ ��ǥ

    public LayerMask WhatIsGround;  // ���� ���̾ �޾ƿ�
    public float CheckSizeX;        // üũ�ڽ��� ũ�⸦ ���� (����)
    public float CheckSizeY;        // üũ�ڽ��� ũ�⸦ ���� (����)

    private void Start()
    {
        player = GetComponent<Player>();

    }

    private void Update()
    {
        if (Time.timeScale == 0.1f)
            return;

        // ���� �� ��� �ִ��� üũ
        groundCheck = new Vector2(GetComponent<BoxCollider2D>().bounds.center.x, GetComponent<BoxCollider2D>().bounds.min.y);
        isGrounded = Physics2D.OverlapBox(groundCheck, new Vector2(CheckSizeX, CheckSizeY), 0f, WhatIsGround);

        if (isGrounded)
        {
            jumpCnt = 2;
        }
        // üũ�� ������ �÷��̾� ���� ��ȯ
        JumpStateSelector();

        // ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �ϴ� ����
            if (Input.GetKey(KeyCode.S) && player.State != PlayerState.Die)
            {
                // ���鿡 ���� �� �������� ��������
                if (isGrounded && isGrounded.CompareTag("Floor"))
                {
                    jumpCnt--;
                    StartCoroutine(DownJump());
                }
            }
            // ���� �� ��� ������ ������ ���°� �ƴ� �� Ȥ�� �뽬 �� �϶� �۵�
            else if (isGrounded && (player.State != PlayerState.Die || player.State == PlayerState.Dash))
            {
                jumpCnt--;
                player.Rigidbody.velocity = new Vector2(player.Rigidbody.velocity.x, 0);
                player.Rigidbody.velocity = new Vector2(0, player.JumpVelocity);
            }
            else if (player.State == PlayerState.Jump && jumpCnt > 0)
            {
                jumpCnt--;
                player.Rigidbody.velocity = new Vector2(player.Rigidbody.velocity.x, 0);
                player.Rigidbody.velocity = new Vector2(0, player.JumpVelocity);
            }
        }
    }

    /// <summary>
    /// �ϴ� ���� ������ �Դϴ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator DownJump()
    {
        getGround = isGrounded.GetComponent<PlatformEffector2D>();
        getGround.colliderMask = getGround.colliderMask ^ 1 << 3;
        yield return new WaitForSecondsRealtime(0.4f);
        getGround.colliderMask = getGround.colliderMask ^ 1 << 3;
    }

    /// <summary>
    /// �÷��̾��� ���� ���¸� ��ȯ�ϴ� �Լ��Դϴ�.
    /// </summary>
    private void JumpStateSelector()
    {
        // ���� �� ���� �ٽ� ������ ��
        if (isGrounded && player.State == PlayerState.Jump)
        {
            player.State = PlayerState.Idle;
        }

        // ���߿� �����鼭 �÷��̾ ������ ���°� �ƴ� ��
        else if (!isGrounded && player.State != PlayerState.Die && player.State != PlayerState.Dash)
        {
            player.State = PlayerState.Jump;
        }
    }
    private void OnDrawGizmos()
    {
        // üũ ���� �ð�ȭ
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck, new Vector2(CheckSizeX, CheckSizeY));
    }
}
