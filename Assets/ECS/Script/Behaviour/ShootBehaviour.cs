using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootBehaviour : MonoBehaviour, IBehaviour
{
    //���������
    public HealtComponent HealtComponent;
    private HealtEnemyComponent thisHealtComponent;
    [SerializeField] private float activDistance = 5f;

    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform outBullet;
    [SerializeField] private ParticleSystem gunExitParticle;//������� ������
    public float ShootDelay;
    private float shootTime = float.MinValue;

    private NavMeshAgent agent;
    private float currentVelocity;
    private float controlDistance;

    [SerializeField] private float correctivAngle, polyrAngle = 1;
    private Vector3 target, currentPosition, distanceVector;
    private float rezulAxisY;
    private void Start()
    {
        HealtComponent = FindObjectOfType<HealtComponent>();//������ ������ � ������ �����������
        thisHealtComponent = GetComponent<HealtEnemyComponent>();
        //Nav
        agent = GetComponent<NavMeshAgent>();
    }
    public void Behaver()
    {
        if (thisHealtComponent.Dead != true)
        {
            if (controlDistance <= activDistance)
            {
                if (Time.time < shootTime + ShootDelay)
                {
                    return;
                }
                else
                {
                    shootTime = Time.time;
                }

                Instantiate(bullet, outBullet.position, outBullet.rotation);
                gunExitParticle.Play();
            }
            //
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
        if (HealtComponent == null || HealtComponent.Dead==true)
        {
            return 0;
        }

        currentVelocity = Mathf.Abs(agent.velocity.magnitude);
        controlDistance = (this.gameObject.transform.position - HealtComponent.transform.position).magnitude;

        if (currentVelocity < 0.1f && (controlDistance <= activDistance))
        {
            return activDistance;
        }
        else
        {
            return 0;
        }



    }
}