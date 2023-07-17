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
            (Entity entity, Transform transform, ref InputData inputData, ref MoveData moveData) =>
            {
                if (transform==null)
                {
                    return;
                }
                else
                {
                    Vector3 currentPosition = transform.position;
                    currentPosition += new Vector3(inputData.Move.x * moveData.MoveSpeed,
                                                   0,
                                                   inputData.Move.y * moveData.MoveSpeed);
                    transform.position = currentPosition;
                }
                

                //
                var dir = new Vector3(inputData.Move.x, 0, inputData.Move.y);
                
                if (dir == Vector3.zero)
                {
                    return;
                }
                else
                {
                    var rot = transform.rotation;
                    var newRot = Quaternion.LookRotation(Vector3.Normalize(dir));
                    Debug.Log(newRot);
                    if (newRot == rot)
                    {
                        return;
                    }
                    else
                    {
                        transform.rotation = Quaternion.Lerp(rot, newRot, Time.DeltaTime * 10);
                    }
                }

            }
            );

    }
}
