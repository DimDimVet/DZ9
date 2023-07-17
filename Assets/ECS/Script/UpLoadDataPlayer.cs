using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpLoadDataPlayer : MonoBehaviour
{
    //UI
    [SerializeField] private Text saveData;
    [SerializeField] private Text loadLocalData;
    [SerializeField] private Text loadFireBaseData;
    //
    [SerializeField] private HealtComponent healtComponent;
    private string hashKey = "DataPlayer";

    //Zenject
    private IData dataConfig;

    [Inject]
    public void Init(IData d)
    {
        dataConfig = d;
    }

    private void Awake()
    {
        StartCoroutine(Example());
    }

    private IEnumerator Example()
    {
        int i = 0;
        while (i < 3)
        {
            FireBaseTool.LoadData(hashKey);
            yield return new WaitForSeconds(0.2f);
            i++;
        }
        LoadData();
    }
    private void LoadData()
    {
        DataPlayer dataPlayerFireBase;
        DataPlayer dataPlayerLocal;
        DataPlayer dataPlayerDefault;

        bool isFireBase=dataConfig.LoadDataFireBase(FireBaseTool.Snapshot, out dataPlayerFireBase);//�������� FireBase
        loadFireBaseData.text = $"healtPlayer={dataPlayerFireBase.healtPlayer} shootCount={dataPlayerFireBase.shootCount}";

        bool isLocalBase = dataConfig.LoadDataLocalBase(hashKey, out dataPlayerLocal);//�������� LocalBase
        loadLocalData.text = $"healtPlayer={dataPlayerLocal.healtPlayer} shootCount={dataPlayerLocal.shootCount}";

        dataConfig.LoadDataDefault(out dataPlayerDefault);

        if (isFireBase)//������� �������� ��������
        {
            GetData(dataPlayerFireBase);
        }
        else if (isLocalBase)//������� �������� ��������
        {
            GetData(dataPlayerLocal);
        }
        else//������� �������� ��������
        {
            GetData(dataPlayerDefault);
        }

    }
    private void GetData(DataPlayer dataPlayer)//��������� ������� ������������
    {
        healtComponent.Healt = dataPlayer.healtPlayer;
        Statistic.ShootCount = dataPlayer.shootCount;//���������� � ���������� ������
    }

    void OnApplicationQuit()
    {
        SaveData();
        //
    }

    private void SaveData()
    {
        DataPlayer dataPlayer = new DataPlayer
        {
            healtPlayer = healtComponent.Healt,
            shootCount = Statistic.ShootCount
        };

        string rezult = dataConfig.SaveData(dataPlayer, hashKey);
        saveData.text = rezult;
    }
}

//srukture 
public struct DataPlayer
{
    public int shootCount;
    public int healtPlayer;
}
