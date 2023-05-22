using System;
using Persistence;
using DAL;

namespace BL
{
    public class MobilePhoneBLL
    {
        private MobilePhoneDAL mobilePDAL = new MobilePhoneDAL();
        public int? AddPhone(MobilePhone mobilePhone) => mobilePDAL.AddPhone(mobilePhone);
        public List<MobilePhone> DisplayAllPhone(int key) => mobilePDAL.DisplayALLMobilePhoneDAL(key);
        public MobilePhone SearchById(int id) => mobilePDAL.GetByIdDAL(id);
        public List<MobilePhone> SearchByName(string name, string value , int key) => mobilePDAL.SreachByOpretingSDAL(name , value , key);
        public int? CountPhone() => mobilePDAL.CountDAL();
    }
    
}