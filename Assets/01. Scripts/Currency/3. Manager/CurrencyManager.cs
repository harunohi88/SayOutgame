using System;
using System.Collections.Generic;
using System.Linq;
using Unity.FPS.Game;
using UnityEngine;

// ��Ű��ó : ���� �� ��ä(���踶�� ö���� �ִ�.)
// ������ ���� : ���踦 �����ϴ� �������� ���̴� ����

public class CurrencyManager : BehaviourSingleton<CurrencyManager>
{
    private Dictionary<ECurrencyType, Currency> _currenyDic;

    // �����ο� ��ȭ�� ���� �� ȣ��Ǵ� �׼�
    public Action OnDataChanged;

    private CurrencyRepository _repository;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _repository = new CurrencyRepository();

        List<CurrencyDTO> loadedCurrencyList = _repository.Load();
        _currenyDic = new();
        if(loadedCurrencyList == null)
        {
            _currenyDic = new();
            for (int i = 0; i < (int)ECurrencyType.Count; i++)
            {
                ECurrencyType type = (ECurrencyType)i;
                // ���, ���̾Ƹ�� ���� 0 ������ ����
                Currency curreny = new Currency(type, 0);
                _currenyDic.Add(type, curreny);
            }
        }
        else
        {
            foreach (CurrencyDTO data in loadedCurrencyList)
            {
                Currency currency = new Currency(data.Type, data.Value);
                _currenyDic.Add(currency.Type, currency);
            }
        }

    }

    private List<CurrencyDTO> ToDTOList()
    {
        return _currenyDic.ToList().ConvertAll(x => new CurrencyDTO(x.Value));
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currenyDic[type]);
    }

    // ������ ��ȿ�� �˻�� �����ο� �־����, �Ŵ����� ������ �ȵȴ�.
    public void Add(ECurrencyType type, int value)
    {
        _currenyDic[type].Add(value);

        AchievementEvent achieveEvent = Events.AchievementEvent;
        achieveEvent.condition = EAchievementCondition.GoldCollect;
        achieveEvent.value = value;
        EventManager.Broadcast(achieveEvent);

        _repository.Save(ToDTOList());
        OnDataChanged?.Invoke();
    }

    public bool TryBuy(ECurrencyType type, int value)
    {
        if (_currenyDic[type].TryBuy(value))
        {
            return false;
        }

        _repository.Save(ToDTOList());
        OnDataChanged?.Invoke();
        return true;
    }
}
