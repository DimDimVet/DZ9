using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserAnimSystem : ComponentSystem
{
    private EntityQuery animQuery;//создадим переменую результата запроса сущностей
    private float refFloat = 0.01f;
    protected override void OnCreate()
    {
        //получим результат запроса всех сущностей имеющие InputData и UserInputDataComponent
        //animQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
        //                           ComponentType.ReadOnly<UserInputDataComponent>());
        animQuery = GetEntityQuery(ComponentType.ReadOnly<AnimData>(), ComponentType.ReadOnly<Animator>(),
                                   ComponentType.ReadOnly<HealtComponent>());
    }
    protected override void OnUpdate()
    {
        //при каждом кадре ищем в сущностях изменеия структуры InputData
        Entities.With(animQuery).ForEach
            (
            //(Entity entity, UserInputDataComponent userInput, ref InputData inputData) =>
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


                    if (healt.Dead)
                    {
                        animator.SetBool(userInput.AnimDead, true);
                    }

                    ////pull
                    ////реакция на изменеия, запустим анимацию 
                    //bool isPull;
                    //if (inputData.Pull > 0f)
                    //{
                    //    isPull = true;
                    //}
                    //else
                    //{
                    //    isPull = false;
                    //}
                    //ability.GetPull(isPull);

                    ////move
                    ////реакция на изменеия, запустим анимацию
                    //float2 move; 
                    //if (inputData.Move.x != 0f | inputData.Move.y != 0f)
                    //{
                    //    move = inputData.Move;
                    //}
                    //else
                    //{
                    //    move.x = 0f;
                    //    move.y = 0f;
                    //}
                    //ability.GetMove(move);

                
            }
            );
    }
}
