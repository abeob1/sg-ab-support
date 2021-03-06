﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IMAP.SupportPortalService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SupportPortalService.SupportSoap")]
    public interface SupportSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string HelloWorld();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LogIn_OLD", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void LogIn_OLD(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LogIn", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void LogIn(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCustomerTickets", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetCustomerTickets(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllTickets", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetAllTickets();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetConsultantTickets", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetConsultantTickets(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllUsers", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllConsultants", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetAllConsultants();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AssignTickets", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void AssignTickets(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreateUser", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void CreateUser(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UpdateUserStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void UpdateUserStatus(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetTicketDetails", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetTicketDetails(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreateTicket", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void CreateTicket(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreateTicketThread", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void CreateTicketThread(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetUnassignedTickets", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetUnassignedTickets();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAccountType", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetAccountType();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllTicketStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetAllTicketStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UpdateTicketStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void UpdateTicketStatus(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetTicketDueDate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void SetTicketDueDate(string sJasonInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetMailNumber", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetMailNumber();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCustomerDashboardData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void GetCustomerDashboardData(string sJasonInput);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SupportSoapChannel : IMAP.SupportPortalService.SupportSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SupportSoapClient : System.ServiceModel.ClientBase<IMAP.SupportPortalService.SupportSoap>, IMAP.SupportPortalService.SupportSoap {
        
        public SupportSoapClient() {
        }
        
        public SupportSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SupportSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SupportSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SupportSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloWorld() {
            return base.Channel.HelloWorld();
        }
        
        public void LogIn_OLD(string sJasonInput) {
            base.Channel.LogIn_OLD(sJasonInput);
        }
        
        public void LogIn(string sJasonInput) {
            base.Channel.LogIn(sJasonInput);
        }
        
        public void GetCustomerTickets(string sJasonInput) {
            base.Channel.GetCustomerTickets(sJasonInput);
        }
        
        public void GetAllTickets() {
            base.Channel.GetAllTickets();
        }
        
        public void GetConsultantTickets(string sJasonInput) {
            base.Channel.GetConsultantTickets(sJasonInput);
        }
        
        public void GetAllUsers() {
            base.Channel.GetAllUsers();
        }
        
        public void GetAllConsultants() {
            base.Channel.GetAllConsultants();
        }
        
        public void AssignTickets(string sJasonInput) {
            base.Channel.AssignTickets(sJasonInput);
        }
        
        public void CreateUser(string sJasonInput) {
            base.Channel.CreateUser(sJasonInput);
        }
        
        public void UpdateUserStatus(string sJasonInput) {
            base.Channel.UpdateUserStatus(sJasonInput);
        }
        
        public void GetTicketDetails(string sJasonInput) {
            base.Channel.GetTicketDetails(sJasonInput);
        }
        
        public void CreateTicket(string sJasonInput) {
            base.Channel.CreateTicket(sJasonInput);
        }
        
        public void CreateTicketThread(string sJasonInput) {
            base.Channel.CreateTicketThread(sJasonInput);
        }
        
        public void GetUnassignedTickets() {
            base.Channel.GetUnassignedTickets();
        }
        
        public void GetAccountType() {
            base.Channel.GetAccountType();
        }
        
        public void GetAllTicketStatus() {
            base.Channel.GetAllTicketStatus();
        }
        
        public void UpdateTicketStatus(string sJasonInput) {
            base.Channel.UpdateTicketStatus(sJasonInput);
        }
        
        public void SetTicketDueDate(string sJasonInput) {
            base.Channel.SetTicketDueDate(sJasonInput);
        }
        
        public void GetMailNumber() {
            base.Channel.GetMailNumber();
        }
        
        public void GetCustomerDashboardData(string sJasonInput) {
            base.Channel.GetCustomerDashboardData(sJasonInput);
        }
    }
}
