using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyRepository
{
    // Repository : �������� ���Ӽ� ����
    // ���Ӽ� : ���α׷��� �����ص� �����Ͱ� �����Ǵ� ��
    // Save / Load

    private const string SAVE_KEY = nameof(CurrencyRepository);
    
    public void Save(List<CurrencyDTO> dataList, string id)
    {
        SaveDatas<CurrencySaveData> datas = new SaveDatas<CurrencySaveData>();
        datas.DataList = dataList.ConvertAll(data => new CurrencySaveData
        {
            Type = data.Type,
            Value = data.Value
        });

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY + "_" + id, json);
    }

    public List<CurrencyDTO> Load(string id)
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY + "_" + id))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY + "_" + id);
        SaveDatas<CurrencySaveData> datas = JsonUtility.FromJson<SaveDatas<CurrencySaveData>>(json);

        return datas.DataList.ConvertAll(data => new CurrencyDTO(data.Type, data.Value));
    }
}

[Serializable]
public struct CurrencySaveData
{
    public ECurrencyType Type;
    public int Value;
}

//[Serializable]
//public class CurrencySaveDatas
//{
//    public List<CurrencySaveData> DataList;
//}
