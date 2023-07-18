using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowComponent : MonoBehaviour,IAngleAction
{
    //
    [Header("Указать основную камеру")]
    [SerializeField] private Camera mainCamera;//получим область экрана.
    [Header("Указать объект слежения")]
    [SerializeField] private Transform visiualTarget;//укажем объект-прицел
    [Header("Поправка высоты линии объекта слежения")]
    [SerializeField] private float popravkaZ = 0f;//поправим высоту стрельбы.

    private Vector3 currentPositionGun, distanceVector, mousePosition;
    private float rezulAxisZ;

    private Vector3 GetPositionInTarget()//метод передачи позиции курсора в target объект
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);//получили координаты мыши
        Debug.Log(mousePosition);
        mousePosition.z = popravkaZ;//откорректировали ось Z
        visiualTarget.transform.position = mousePosition;//передали координаты к объекту "прицел"
        return visiualTarget.transform.position;
    }
    public void FollowTarget()//метод слежение за target
    {
        currentPositionGun = transform.position;//проверим текущию позицию Gun
        distanceVector = GetPositionInTarget() - currentPositionGun;//вычислим вектор между Gun-target
        rezulAxisZ = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;//вычислим угол вектора в градусах

        //if (rezulAxisZ <= maxH & rezulAxisZ >= minH)//проверим угол поворота Gun на +четверть
        //{
            transform.rotation = Quaternion.Euler(0, 0, rezulAxisZ);//повернем Gun
        //}

        //if (rezulAxisZ <= minL & rezulAxisZ >= maxL)//проверим угол поворота Gun на -четверть
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, rezulAxisZ);//повернем Gun
        //}
    }

    //void Update()
    //{
    //    FollowTarget();
    //}
}
