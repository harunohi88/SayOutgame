using UnityEngine;
using System;
using System.Text.RegularExpressions;
using Unity.Collections;

public class Account
{
    public readonly string Email;
    public readonly string NickName;
    public readonly string Password;

    public Account(string email, string nickName, string password)
    {
        // ��Ģ�� ��ü�� ĸ��ȭ�ؼ� �и��Ѵ�.
        // �׷��� �����ΰ� UI�� ��� "�� ��Ģ�� �����ϴ�?" ������ �ȴ�.
        // ĸ��ȭ�� ��Ģ : ��
        if(CheckSpecification(email, nickName, password))
        {
            Email = email;
            NickName = nickName;
            Password = password;
        }
    }

    private bool CheckSpecification(string email, string nickName, string password)
    {
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickName))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            throw new Exception(passwordSpecification.ErrorMessage);
        }

        return true;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(Email, NickName, Password);
    }
}