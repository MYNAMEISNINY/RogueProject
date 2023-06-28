using UnityEngine;

public abstract class Enemy : Character
{
    private float gravity;

    public float JumpVelocity;

    public Player player;

    protected virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        PhysicsInit();
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
}