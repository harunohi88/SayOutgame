using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class AccountManager : BehaviourSingleton<AccountManager>
{
    private Account _myAccount;
    public AccountDTO CurrencAccount => _myAccount.ToDTO();

    private AccountRepository _accountRepository;
    private const string SALT = "12315";

    private void Awake()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        _accountRepository = new AccountRepository();
    }

    public Result TryRegister(string email, string nickname, string password)
    {
        AccountDTO accountDTO = _accountRepository.Find(email);
        if(accountDTO != null)
        {
            return new Result(false, "�̹� ������ �̸����Դϴ�.");
        }

        // ��й�ȣ ��Ģ ����

        string encryptedPassword = CryptoUtil.Encryption(password, SALT);
        Account account = new Account(email, nickname, encryptedPassword);
        _accountRepository.Save(account.ToDTO());

        return new Result(true);
    }

    public bool TryLogin(string email, string password)
    {
        AccountDTO accountDTO = _accountRepository.Find(email);
        if (accountDTO == null) return false;

        if (CryptoUtil.Verify(password, accountDTO.Password, SALT))
        {
            _myAccount = new Account(accountDTO.Email, accountDTO.NickName, accountDTO.Password);
            return true;
        }

        return false;
    }

    public string GetCurrentEmail()
    {
        if(_myAccount == null)
        {
            throw new System.Exception("���� ������ ������� �ʾҽ��ϴ�.");
        }

        if (_myAccount.Email.IsNullOrEmpty())
        {
            throw new System.Exception("�̸����� �ùٸ��� �ʽ��ϴ�.");
        }
        return _myAccount.Email;
    }
}
