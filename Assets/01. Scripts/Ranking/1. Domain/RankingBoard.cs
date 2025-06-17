using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.Tutorials.Core.Editor;
using NUnit.Framework.Constraints;
using System;

public class RankingBoard
{
    private Dictionary<string, Ranking> _rankDic;

    public RankingBoard()
    {
        _rankDic = new Dictionary<string, Ranking>();
    }

    public RankingBoard(Dictionary<string, Ranking> rankDic)
    {
        _rankDic = rankDic;
    }

    public List<RankingDTO> GetSortedRankList(int count)
    {
        return _rankDic.Values
            .Select(rank => rank.ToDTO())
            .OrderByDescending(dto => dto.KillCount)
            .Take(count)
            .ToList();
    }

    private List<Ranking> GetSortedRankList()
    {
        return _rankDic.Values
            .OrderByDescending(dto => dto.KillCount)
            .ToList();
    }

    public void UpdateRank(string email, int killCount)
    {
        if (email.IsNullOrEmpty())
        {
            throw new Exception("�̸����� ������� �� �����ϴ�.");
        }

        if(_rankDic.TryGetValue(email, out Ranking rank))
        {
            rank.AddKillCount(killCount);
        }
        else
        {
            _rankDic.Add(email, new Ranking(email, killCount));
        }
    }

    public RankingDTO GetRankDataByEmail(string email)
    {
        if (email.IsNullOrEmpty())
        {
            throw new Exception("�̸����� ������� �� �����ϴ�.");
        }

        if (_rankDic.TryGetValue(email, out Ranking rank))
        {
            return rank.ToDTO();
        }
        else
        {
            _rankDic.Add(email, new Ranking(email, 0));
            return _rankDic[email].ToDTO();
        }

    }

    public int GetRankNumberByEmail(string email)
    {
        if (email.IsNullOrEmpty())
        {
            throw new Exception("�̸����� ������� �� �����ϴ�.");
        }

        if (_rankDic.ContainsKey(email) == false)
        {
            _rankDic.Add(email, new Ranking(email, 0));
        }

        return GetSortedRankList().FindIndex(x => x.Email == email) + 1;
    }

    public RankingBoardDTO ToDTO()
    {
        return new RankingBoardDTO(
        _rankDic.ToDictionary(pair => pair.Key, pair => pair.Value.ToDTO()));
    }
}
