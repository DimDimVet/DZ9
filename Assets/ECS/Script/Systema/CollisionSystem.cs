using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionSystem : ComponentSystem
{
    private EntityQuery entityQuery;//�������� ��������� ���������� ������� ���������
    private Collider[] result = new Collider[50];//��� ��������� ������� ��� ��������� ����������� ��������
    protected override void OnCreate()
    {
        //������� ��������� ������� ���� ��������� ������� ColliderData � Transform
        entityQuery = GetEntityQuery(ComponentType.ReadOnly<ColliderData>(),
                                     ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //������� �� ������� �������� ������� ������ ���������� ��������
        Entities.With(entityQuery).ForEach
            (
            (Entity entity, CollisionsComponent collisions, ref ColliderData colliderData) =>
            {
                if (collisions!=null)
                {
                    GameObject gameObject = collisions.gameObject;//�������� gameObject �������� ������� CollisionsComponent
                    float3 position = gameObject.transform.position;//�������� float3 ������� �������� ������� CollisionsComponent
                    Quaternion rotation = gameObject.transform.rotation;//�������� Quaternion ������� �������� ������� CollisionsComponent


                    int size = 0;

                    switch (colliderData.TypeCollider)//������� ��������� ColliderData ����� �������� 
                    {
                        case TypeCollider.Capsule:
                            float3 center = ((colliderData.CapsuleStart + position) + (colliderData.CapsuleEnd + position)) / 2f;
                            float3 point1 = colliderData.CapsuleStart + position;
                            float3 point2 = colliderData.CapsuleEnd + position;
                            point1 = (float3)(rotation * (point1 - center)) + center;
                            point2 = (float3)(rotation * (point2 - center)) + center;
                            size = Physics.OverlapCapsuleNonAlloc(point1, point2, colliderData.CapsuleRadius, result);
                            break;

                        case TypeCollider.Sphere:
                            size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position, colliderData.SphereRadius, result);
                            break;

                        case TypeCollider.Box:
                            size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position, colliderData.BoxHalfExtents,
                                                              result, colliderData.BoxOrientation * rotation);
                            break;

                        default:
                            break;
                    }

                    if (size > 0)//���� ��������� Physics.Overlap ����� 0
                    {
                        List<Collider> colliders = new List<Collider>();//��� ������ ������� �������������� ����
                        for (int i = 0; i < result.Length; i++)
                        {
                            if (result[i] != null)//������� ��� �������� �������
                            {
                                colliders.Add(result[i]);//�� ��� ��������, ������� � ����
                            }

                        }
                        //������� ����� � ��������(����������) ������� CollisionsComponent � � ������������� ICollisionsComponent
                        //� ��������� ���� ��������� ���� ���������� ���������
                        collisions.Execute(colliders);
                    }
                }
                
            }
            );
    }
}
