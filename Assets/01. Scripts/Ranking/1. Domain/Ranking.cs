using Unity.Tutorials.Core.Editor;
using UnityEngine;
using System;

public class Ranking
{
    public readonly string Email;
    private int _killCount;
    public int KillCount => _killCount;

    public Ranking(string email, int killCount)
    {
        if (email.IsNullOrEmpty())
        {
            throw new Exception("�̸����� ������� �� �����ϴ�.");
        }
        if(killCount < 0)
        {
            throw new Exception("ųī��Ʈ�� ������ �� �����ϴ�.");
        }

        Email = email;
        _killCount = killCount;
    }

    public void AddKillCount(int count)
    {
        if(count <= 0)
        {
            throw new Exception("ī��Ʈ�� 0 ������ �� �����ϴ�.");
        }

        _killCount += count;
    }

    public RankingDTO ToDTO()
    {
        return new RankingDTO(Email, _killCount);
    }
}
