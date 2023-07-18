using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class UserMoveSystem : ComponentSystem
{
    private EntityQuery moveQuery;

    protected override void OnCreate()
    {
        moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
                                   ComponentType.ReadOnly<MoveData>(),
                                   ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(moveQuery).ForEach
            (
            (Entity entity, UserInputDataComponent userInput, /*Transform transform,*/ ref InputData inputData, ref MoveData moveData) =>
            {
                if (userInput.FollowAction != null && userInput.FollowAction is IAngleAction ability)
                {
                    ability.FollowTarget();
                }
               

                //if (transform == null)
                //{
                //    return;
                //}
                //else
                //{
                //    Vector3 currentPosition = transform.position;

                //    currentPosition += new Vector3(inputData.Move.x * moveData.MoveSpeed,
                //                                   0,
                //                                   inputData.Move.y * moveData.MoveSpeed);
                //    transform.position = currentPosition;

                    //

                    //var dir = new Vector3(inputData.Move.x, 0, inputData.Move.y);

                    //if (dir == Vector3.zero)
                    //{
                    //    return;
                    //}

                    //var rot = transform.rotation;
                    //rot = new Quaternion(rot.x, rot.y, rot.z, rot.w); //+ moveData.MoveAngle
                    //var newRot = Quaternion.LookRotation(Vector3.Normalize(dir));
                    //Debug.Log($"newRot{newRot}  rot{rot}");

                    //if (newRot == rot)
                    //{
                    //    return;
                    //}

                    //transform.rotation = Quaternion.Lerp(rot, newRot, Time.DeltaTime * 10);

                //}


                //private Vector3 GetPositionInTarget()//����� �������� ������� ������� � target ������
                //{
                //    mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);//�������� ���������� ����
                //    mousePosition.z = popravkaZ;//���������������� ��� Z
                //    visiualTarget.transform.position = mousePosition;//�������� ���������� � ������� "������"
                //    return visiualTarget.transform.position;
                //}
                //private void FollowTarget()//����� �������� �� target
                //{
                //    currentPositionGun = transform.position;//�������� ������� ������� Gun
                //    distanceVector = GetPositionInTarget() - currentPositionGun;//�������� ������ ����� Gun-target
                //    rezulAxisZ = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;//�������� ���� ������� � ��������

                //    if (rezulAxisZ <= maxH & rezulAxisZ >= minH)//�������� ���� �������� Gun �� +��������
                //    {
                //        transform.rotation = Quaternion.Euler(0, 0, rezulAxisZ);//�������� Gun
                //    }

                //    if (rezulAxisZ <= minL & rezulAxisZ >= maxL)//�������� ���� �������� Gun �� -��������
                //    {
                //        transform.rotation = Quaternion.Euler(0, 0, rezulAxisZ);//�������� Gun
                //    }
                //}

                //void Update()
                //{
                //    FollowTarget();
                //}

            }
            );
    }

}
