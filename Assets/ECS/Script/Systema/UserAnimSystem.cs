using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserAnimSystem : ComponentSystem
{
    private EntityQuery animQuery;//создадим переменую результата запроса сущностей
    private float refFloat = 0.01f;
    protected override void OnCreate()
    {
        //получим результат запроса всех сущностей 
        animQuery = GetEntityQuery(ComponentType.ReadOnly<AnimData>(), ComponentType.ReadOnly<Animator>(),
                                   ComponentType.ReadOnly<HealtComponent>());
    }
    protected override void OnUpdate()
    {
        //при каждом кадре ищем в сущностях изменеия структуры InputData
        Entities.With(animQuery).ForEach
            (
            (Entity entity, ref InputData inputData, Animator animator, UserInputDataComponent userInput, HealtComponent healt) =>
            {

                //move
                //реакция на изменеия, запустим анимацию

                if (Mathf.Abs(inputData.Move.x) >= refFloat | Mathf.Abs(inputData.Move.y) >= refFloat)
                {
                    animator.SetFloat(userInput.AnimSpeed, 1);
                }
                else
                {
                    animator.SetFloat(userInput.AnimSpeed, 0);
                }

                //pull
                //реакция на изменеия, запустим анимацию 
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
