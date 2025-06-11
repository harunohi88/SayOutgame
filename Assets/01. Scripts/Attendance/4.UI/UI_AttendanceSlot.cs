using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_AttendanceSlot : MonoBehaviour
{
    private int _day;
    public TextMeshProUGUI AttendanceInfoText;
    public TextMeshProUGUI RewardInfoText;
    public TextMeshProUGUI RewardAmountText;
    public Button RewardButton;

    public void Init(int day, AttendanceCalendarDTO dto, AttendanceSO so)
    {
        _day = day;
        if (dto.Entries[day].IsRewardClaimed)
        {
            AttendanceInfoText.text = "ȹ�� �Ϸ�";
            RewardButton.interactable = false;
        }
        else if (dto.Entries[day].IsChecked)
        {
            AttendanceInfoText.text = "ȹ�� ����";
            RewardButton.interactable = false;
        }
        else
        {
            AttendanceInfoText.text = $"{day}����";
            RewardButton.interactable = true;
        }

        if (so.Attendances[day - 1].Type == ECurrencyType.Gold) RewardInfoText.text = "���";
        else if (so.Attendances[day - 1].Type == ECurrencyType.Diamond) RewardInfoText.text = "���̾�";

        RewardAmountText.text = so.Attendances[day - 1].Value.ToString();
    }

    public void Refresh(AttendanceCalendarDTO attendance)
    {
        if (attendance.Entries[_day].IsRewardClaimed)
        {
            AttendanceInfoText.text = "ȹ�� �Ϸ�";
            RewardButton.interactable = false;
        }
        else if (attendance.Entries[_day].IsChecked)
        {
            AttendanceInfoText.text = "ȹ�� ����";
            RewardButton.interactable = false;
        }
        else
        {
            AttendanceInfoText.text = $"{_day}����";
            RewardButton.interactable = true;
        }
    }
}
