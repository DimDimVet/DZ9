using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageBlockCollision : MonoBehaviour, ICollisionsComponent
{
    [SerializeField] private int damage = 10000;
    [SerializeField] private Transform target;
    [SerializeField] private float duration;

    private bool isStop = false;
    private void Start()
    {
        Sequence sequence = DOTween.Sequence();//������� DOTween
        sequence.Append(transform.DOMoveY (target.position.y, duration, false)).SetLoops(-1, LoopType.Yoyo);//
        //����� Append ������ ��� �������� target.position.y - ���� �����������, duration - ����� ����������,
        //false - ��� float, true - ��� int, SetLoops(-1, LoopType.Yoyo)- ����� �����(������ -1(�������������) �����
        //����� ������� ���������� ����� ��������

    }


    public void Execute(List<Collider> colliders)
    {
        if (isStop == false)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                HealtComponent healt = colliders[i].GetComponent<HealtComponent>();
                if (healt != null)
                {
                    healt.HealtContoll(damage);
                    isStop = true;
                }
                else
                {
                    isStop = false;
                }
            }
            

        }
    }

}
