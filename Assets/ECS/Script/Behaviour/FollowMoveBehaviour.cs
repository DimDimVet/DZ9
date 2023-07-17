using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowMoveBehaviour : MonoBehaviour, IBehaviour
{
    public HealtComponent HealtComponent;
    private HealtEnemyComponent thisHealtComponent;
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float activDistance = 5f;
    //���������
    private NavMeshAgent agent;
    private Vector3 pointDefault;
    private float controlDistance;
    private float currentVelocity;

    private Animator animator;

    [SerializeField] private float correctivAngle, polyrAngle = 1;
    private Vector3 target, currentPosition, distanceVector;
    private float rezulAxisY;
    private void Start()
    {
        HealtComponent = FindObjectOfType<HealtComponent>();//������ ������ � ������ �����������
        thisHealtComponent = GetComponent<HealtEnemyComponent>();
        //Nav
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        pointDefault = this.gameObject.transform.position;
    }
    public void Behaver()
    {
        if (thisHealtComponent.Dead != true)
        {

            agent.stoppingDistance = stopDistance;
            currentVelocity = Mathf.Abs(agent.velocity.magnitude);

            if (controlDistance <= activDistance)
            {
                agent.destination = HealtComponent.transform.position;//Player ������� � ����

            }
            else
            {
                agent.destination = pointDefault;
            }

            if (currentVelocity > 0.1f)
            {
                animator.SetFloat("SpeedEnemy", 1);
            }
            else
            {
                animator.SetFloat("SpeedEnemy", 0);
            }

            target = HealtComponent.transform.position;//Player ������� � ����
            currentPosition = this.gameObject.transform.position;//�������� ������� ������� Gun
            distanceVector = target - currentPosition;//�������� ������ ����� Gun-target
            rezulAxisY = Mathf.Atan2(distanceVector.x, distanceVector.z) * Mathf.Rad2Deg * polyrAngle;//�������� ���� ������� � ��������
            this.gameObject.transform.rotation = Quaternion.Euler(0, (rezulAxisY + correctivAngle), 0);//�������� Gun angleX
        }
        else
        {
            agent.enabled = false;
        }

    }

    public float Evaluete()
    {
        if (HealtComponent == null || HealtComponent.Dead == true)
        {
            return 0;
        }

        //return 1 / (this.gameObject.transform.position - HealtComponent.transform.position).magnitude;//��� ����� �������� ����
        controlDistance = (this.gameObject.transform.position - HealtComponent.transform.position).magnitude;
        if (currentVelocity > 0.1f)
        {
            return Mathf.Abs(controlDistance);
        }
        else
        {
            return 0;
        }

    }

}
