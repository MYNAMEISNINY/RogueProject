using UnityEngine;

public enum PlayerState { Idle = 0, Run, Jump, Dash, Die }

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : Character
{
    // �뽬 �ӵ�
    public float DashVelocity = 15f;
    // �뽬 �ð�
    public float DashTime = 0.1f;
    // �÷��̾��� ����
    public PlayerState State = PlayerState.Idle;

    public SpriteRenderer SpriteRender;


    #region private field
    // ���� ���� �� ����Ǵ� �ӵ�
    public float JumpVelocity;

    private float gravity;
    #endregion

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        PhysicsInit();
    }

    void Update()
    {
        InputGroup();
    }

    private void PhysicsInit()
    {
        // ������ = �ʱ�ӵ� * �ð� + ���ӵ� * �ð�^2 * 1/2
        // �� ���� ��ü������ �ٲٸ�
        // ��������(jumpHeight) = �߷�(gravity) * �ɸ��ð�(timeToJumpApex)^2 * 1/2
        // �߷��� ������ ������ ������ �������� �����ϸ�
        // �߷� = (2 * ���� ����) / (�ְ� ���̿� �����ϴ� �ð��� ����)
        gravity = (2 * Status.JumpHeight) / Mathf.Pow(Status.TimeToJumpApex, 2);
        Rigidbody.gravityScale = gravity * 10f / 98f;
        // ���� �ӵ� = �߷� * �ְ� ���̿� �����ϴ� �ð�
        JumpVelocity = Mathf.Abs(gravity) * Status.TimeToJumpApex;
    }

    private void InputGroup()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Attack
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Skill 1
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Skill 2
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Skill 3
        }
    }
}