using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Player player;
    private float _dashTimer = 0f;              // �뽬 ���� �ð��� ��� Ÿ�̸�
    private float _dashCooldownTimer = 0f;      // �뽬 ��Ÿ���� ��� Ÿ�̸�
    private Vector2 dashVelocity;              // ���� �뽬 �ӵ����� ���� ����
    private Vector2 dashDirection;
    public float DashCooldown;          // �뽬 ��Ÿ��
    Coroutine g;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Time.timeScale == 0.1f)
            return;

        // �뽬 ��Ÿ�� ����
        if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= Time.unscaledDeltaTime;
        }

        // ���콺 ��Ŭ�� ������ �뽬
        if (Input.GetKeyDown(KeyCode.Mouse1) && _dashCooldownTimer <= 0 && player.State != PlayerState.Die)
        {
            DashInit();
        }
    }

    private void FixedUpdate()
    {
        // �뽬 ������ ���
        if (player.State == PlayerState.Dash)
        {
            // �뽬 ������
            DashMovement();

            // �ð��� �� ������ Idle ���·� ��ȯ
            _dashTimer -= Time.unscaledDeltaTime;
            if (_dashTimer <= 0)
            {
                player.State = PlayerState.Idle;
            }
        }
    }
    /// <summary>
    /// �뽬�� �� �ʿ��� �������� �ʱ�ȭ ���ִ� �Լ��Դϴ�.
    /// </summary>
    private void DashInit()
    {
        player.State = PlayerState.Dash;
        _dashTimer = player.DashTime;
        _dashCooldownTimer = DashCooldown;
        dashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    /// <summary>
    /// �뽬 �������� ������ �Լ��Դϴ�.
    /// </summary>
    private void DashMovement()
    {
        dashVelocity = dashDirection.normalized * player.DashVelocity;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, player.DashVelocity, 1 << 6);
        if (hit)
        {
            if (g == null)
                g = StartCoroutine(IgnoreCollsion(hit));
        }
        player.Rigidbody.velocity = dashVelocity;
    }

    IEnumerator IgnoreCollsion(RaycastHit2D hit)
    {
        PlatformEffector2D getGround = hit.transform.GetComponent<PlatformEffector2D>();
        getGround.colliderMask = getGround.colliderMask ^ 1 << 3;
        yield return new WaitForSecondsRealtime(0.4f);
        getGround.colliderMask = getGround.colliderMask ^ 1 << 3;
        g = null;
    }
}
