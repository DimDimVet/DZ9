using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowComponent : MonoBehaviour,IAngleAction
{
    //
    [Header("������� �������� ������")]
    [SerializeField] private Camera mainCamera;//������� ������� ������.
    [Header("������� ������ ��������")]
    [SerializeField] private Transform visiualTarget;//������ ������-������
    [Header("�������� ������ ����� ������� ��������")]
    [SerializeField] private float popravkaZ = 0f;//�������� ������ ��������.

    private Vector3 currentPositionGun, distanceVector, mousePosition;
    private float rezulAxisZ;

    private Vector3 GetPositionInTarget()//����� �������� ������� ������� � target ������
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);//�������� ���������� ����
        Debug.Log(mousePosition);
        mousePosition.z = popravkaZ;//���������������� ��� Z
        visiualTarget.transform.position = mousePosition;//�������� ���������� � ������� "������"
        return visiualTarget.transform.position;
    }
    public void FollowTarget()//����� �������� �� target
    {
        currentPositionGun = transform.position;//�������� ������� ������� Gun
        distanceVector = GetPositionInTarget() - currentPositionGun;//�������� ������ ����� Gun-target
        rezulAxisZ = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;//�������� ���� ������� � ��������

        //if (rezulAxisZ <= maxH & rezulAxisZ >= minH)//�������� ���� �������� Gun �� +��������
        //{
            transform.rotation = Quaternion.Euler(0, 0, rezulAxisZ);//�������� Gun
        //}

        //if (rezulAxisZ <= minL & rezulAxisZ >= maxL)//�������� ���� �������� Gun �� -��������
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, rezulAxisZ);//�������� Gun
        //}
    }

    //void Update()
    //{
    //    FollowTarget();
    //}
}
