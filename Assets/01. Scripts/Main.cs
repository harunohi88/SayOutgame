using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        // ������(������) : �ذ��ϰ��� �ϴ� ���� ����, ���� ��ü�� �ǹ��Ѵ�.
        // ������ ��(�𵨸�) : �����ΰ� �� ��Ģ�� �߻�ȭ�� ��

        Currency gold = new Currency(ECurrencyType.Gold, 100);
        Currency Diamond = new Currency(ECurrencyType.Diamond, 34);
    }
}
