using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserAnimSystem : ComponentSystem
{
    private EntityQuery animQuery;//�������� ��������� ���������� ������� ���������
    private float refFloat = 0.01f;
    protected override void OnCreate()
    {
        //������� ��������� ������� ���� ��������� 
        animQuery = GetEntityQuery(ComponentType.ReadOnly<AnimData>(), ComponentType.ReadOnly<Animator>(),
                                   ComponentType.ReadOnly<HealtComponent>());
    }
    protected override void OnUpdate()
    {
        //��� ������ ����� ���� � ��������� �������� ��������� InputData
        Entities.With(animQuery).ForEach
            (
            (Entity entity, ref InputData inputData, Animator animator, UserInputDataComponent userInput, HealtComponent healt) =>
            {

                //move
                //������� �� ��������, �������� ��������

                if (Mathf.Abs(inputData.Move.x) >= refFloat | Mathf.Abs(inputData.Move.y) >= refFloat)
                {
                    animator.SetFloat(userInput.AnimSpeed, 1);
                }
                else
                {
                    animator.SetFloat(userInput.AnimSpeed, 0);
                }

                //pull
                //������� �� ��������, �������� �������� 
                if (inputData.Pull > 0f)
                {
                    animator.SetBool(userInput.AnimJamp, true);
                }
                else
                {
                    animator.SetBool(userInput.AnimJamp, false);
                }

                //dead
                if (healt.Dead)
                {
                    animator.SetBool(userInput.AnimDead, true);
                }

            }
            );
    }
}
