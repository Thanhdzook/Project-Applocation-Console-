using System;
using Persistence;
using DAL;

namespace BL
{
    public class AccountBLL
    {
        private AccountDAL accountDAL = new AccountDAL();
        // public Account GetByAccountID(int account_Id) => accountDAL.GetByAccountID(account_Id);
        // public int? CreateAccount(Account account) => accountDAL.CreateAccount(account);
        public Account Login(string username , string password) => accountDAL.LoginDAL(username , password);
        // public int? ChangeStatusAccount(int id,int role) => accountDAL.ChangeStatusAccountDAL(id,role);
        // public int? LockAccount(int id) => accountDAL.LockAcc(id);
        // public int? UnLockAccount(int id) => accountDAL.UnLockAcc(id);
        // public Account DisplayAcount(int id) => accountDAL.GetByIDDAL(id);
        // public List<Account> DisplayAllAccounts() => accountDAL.DisplayALLAccountDAL();
        // public int? Count() => accountDAL.CountDAL();
    }
}