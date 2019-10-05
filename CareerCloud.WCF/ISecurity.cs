using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.WCF
{
    [ServiceContract]
    public interface ISecurity
    {
        [OperationContract]
        void AddSecurityLogin(SecurityLoginPoco[] items);
        [OperationContract]
        void UpdateSecurityLogin(SecurityLoginPoco[] items);
        [OperationContract]
        void RemoveSecurityLogin(SecurityLoginPoco[] items);
        [OperationContract]
        SecurityLoginPoco GetSingleSecurityLogin(String Id);
        [OperationContract]
        List<SecurityLoginPoco> GetAllSecurityLogin();

        [OperationContract]
        void AddSecurityLoginsLog(SecurityLoginsLogPoco[] items);
        [OperationContract]
        void UpdateSecurityLoginsLog(SecurityLoginsLogPoco[] items);
        [OperationContract]
        void RemoveSecurityLoginsLog(SecurityLoginsLogPoco[] items);
        [OperationContract]
        SecurityLoginsLogPoco GetSingleSecurityLoginsLog(String Id);
        [OperationContract]
        List<SecurityLoginsLogPoco> GetAllSecurityLoginsLog();

        [OperationContract]
        void AddSecurityLoginsRole(SecurityLoginsRolePoco[] items);
        [OperationContract]
        void UpdateSecurityLoginsRole(SecurityLoginsRolePoco[] items);
        [OperationContract]
        void RemoveSecurityLoginsRole(SecurityLoginsRolePoco[] items);
        [OperationContract]
        SecurityLoginsRolePoco GetSingleSecurityLoginsRole(String Id);
        [OperationContract]
        List<SecurityLoginsRolePoco> GetAllSecurityLoginsRole();


        //RemoveSecurityLoginsRole

        [OperationContract]
        void AddSecurityRole(SecurityRolePoco[] items);
        [OperationContract]
        void UpdateSecurityRole(SecurityRolePoco[] items);
        [OperationContract]
        void RemoveSecurityRole(SecurityRolePoco[] items);
        [OperationContract]
        SecurityRolePoco GetSingleSecurityRole(String Id);
        [OperationContract]
        List<SecurityRolePoco> GetAllSecurityRole();
    }
}


