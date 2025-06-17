using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UI_AttendanceSlot : MonoBehaviour
{
    public int Day;
    public TextMeshProUGUI AttendanceInfoText;
    public TextMeshProUGUI RewardInfoText;
    public TextMeshProUGUI RewardAmountText;
    public Button RewardButton;

    public void Init(int day, AttendanceCalendarDTO dto, AttendanceSO so, bool isAccumulate)
    {
        Day = day;

        if(isAccumulate == false)
        {
            if (dto.Entries[day].IsRewardClaimed)
            {
                AttendanceInfoText.text = "ȹ�� �Ϸ�";
                RewardButton.interactable = false;
            }
            else if (dto.Entries[day].IsChecked)
            {
                AttendanceInfoText.text = "ȹ�� ����";
                RewardButton.interactable = true;
            }
            else
            {
                AttendanceInfoText.text = $"{day}����";
                RewardButton.interactable = false;
            }
        }
        else
        {
            if (dto.AccumulateEntries[day].IsRewardClaimed)
            {
                AttendanceInfoText.text = "ȹ�� �Ϸ�";
                RewardButton.interactable = false;
            }
            else if (dto.AccumulateEntries[day].IsChecked)
            {
                AttendanceInfoText.text = "ȹ�� ����";
                RewardButton.interactable = true;
            }
            else
            {
                AttendanceInfoText.text = $"{day}ȸ �⼮ ����";
                RewardButton.interactable = false;
            }
        }

        if (so.Attendances[day - 1].Type == ECurrencyType.Gold) RewardInfoText.text = "���";
        else if (so.Attendances[day - 1].Type == ECurrencyType.Diamond) RewardInfoText.text = "���̾�";

        RewardAmountText.text = so.Attendances[day - 1].Value.ToString();
    }

    public void Refresh(AttendanceCalendarDTO attendance, bool isAccumulate)
    {
        if(isAccumulate == false)
        {
            if (attendance.Entries[Day].IsRewardClaimed)
            {
                AttendanceInfoText.text = "ȹ�� �Ϸ�";
                RewardButton.interactable = false;
            }
            else if (attendance.Entries[Day].IsChecked)
            {
                AttendanceInfoText.text = "ȹ�� ����";
                RewardButton.interactable = true;
            }
            else
            {
                AttendanceInfoText.text = $"{Day}����";
                RewardButton.interactable = false;
            }
        }
        else
        {
            if (attendance.AccumulateEntries[Day].IsRewardClaimed)
            {
                AttendanceInfoText.text = "ȹ�� �Ϸ�";
                RewardButton.interactable = false;
            }
            else if (attendance.AccumulateEntries[Day].IsChecked)
            {
                AttendanceInfoText.text = "ȹ�� ����";
                RewardButton.interactable = true;
            }
            else
            {
                AttendanceInfoText.text = $"{Day}ȸ �⼮ ����";
                RewardButton.interactable = false;
            }
        }
    }

    public void OnClickClaimRewardButton()
    {
        AttendanceManager.Instance.TryClaimReward(Day);
    }

    public void OnClickClaimAccumulateRewardButton()
    {
        AttendanceManager.Instance.TryClaimAccumulateReward(Day);
    }
}
