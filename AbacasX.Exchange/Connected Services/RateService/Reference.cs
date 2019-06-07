﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AbacasX.Exchange.RateService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RateService.IRateService", CallbackContract=typeof(AbacasX.Exchange.RateService.IRateServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IRateService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/SubscribeToAssetRateUpdate", ReplyAction="http://tempuri.org/IRateService/SubscribeToAssetRateUpdateResponse")]
        void SubscribeToAssetRateUpdate(string AssetId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/SubscribeToAssetRateUpdate", ReplyAction="http://tempuri.org/IRateService/SubscribeToAssetRateUpdateResponse")]
        System.Threading.Tasks.Task SubscribeToAssetRateUpdateAsync(string AssetId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnSubscribeToAssetRateUpdate", ReplyAction="http://tempuri.org/IRateService/UnSubscribeToAssetRateUpdateResponse")]
        void UnSubscribeToAssetRateUpdate(string AssetId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnSubscribeToAssetRateUpdate", ReplyAction="http://tempuri.org/IRateService/UnSubscribeToAssetRateUpdateResponse")]
        System.Threading.Tasks.Task UnSubscribeToAssetRateUpdateAsync(string AssetId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/SubscribeToCurrencyPairRateUpdate", ReplyAction="http://tempuri.org/IRateService/SubscribeToCurrencyPairRateUpdateResponse")]
        void SubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/SubscribeToCurrencyPairRateUpdate", ReplyAction="http://tempuri.org/IRateService/SubscribeToCurrencyPairRateUpdateResponse")]
        System.Threading.Tasks.Task SubscribeToCurrencyPairRateUpdateAsync(string Currency1, string Currency2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnSubscribeToCurrencyPairRateUpdate", ReplyAction="http://tempuri.org/IRateService/UnSubscribeToCurrencyPairRateUpdateResponse")]
        void UnSubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnSubscribeToCurrencyPairRateUpdate", ReplyAction="http://tempuri.org/IRateService/UnSubscribeToCurrencyPairRateUpdateResponse")]
        System.Threading.Tasks.Task UnSubscribeToCurrencyPairRateUpdateAsync(string Currency1, string Currency2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/SubscribeToTokenPairRateUpdate", ReplyAction="http://tempuri.org/IRateService/SubscribeToTokenPairRateUpdateResponse")]
        void SubscribeToTokenPairRateUpdate(string Token1, string Token2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/SubscribeToTokenPairRateUpdate", ReplyAction="http://tempuri.org/IRateService/SubscribeToTokenPairRateUpdateResponse")]
        System.Threading.Tasks.Task SubscribeToTokenPairRateUpdateAsync(string Token1, string Token2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnSubscribeToTokenPairRateUpdate", ReplyAction="http://tempuri.org/IRateService/UnSubscribeToTokenPairRateUpdateResponse")]
        void UnSubscribeToTokenPairRateUpdate(string Token, string Token2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnSubscribeToTokenPairRateUpdate", ReplyAction="http://tempuri.org/IRateService/UnSubscribeToTokenPairRateUpdateResponse")]
        System.Threading.Tasks.Task UnSubscribeToTokenPairRateUpdateAsync(string Token, string Token2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/SubscribeToTokenRateUpdate", ReplyAction="http://tempuri.org/IRateService/SubscribeToTokenRateUpdateResponse")]
        void SubscribeToTokenRateUpdate(string TokenId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/SubscribeToTokenRateUpdate", ReplyAction="http://tempuri.org/IRateService/SubscribeToTokenRateUpdateResponse")]
        System.Threading.Tasks.Task SubscribeToTokenRateUpdateAsync(string TokenId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnSubscribeToTokenRateUpdate", ReplyAction="http://tempuri.org/IRateService/UnSubscribeToTokenRateUpdateResponse")]
        void UnSubscribeToTokenRateUpdate(string TokenId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnSubscribeToTokenRateUpdate", ReplyAction="http://tempuri.org/IRateService/UnSubscribeToTokenRateUpdateResponse")]
        System.Threading.Tasks.Task UnSubscribeToTokenRateUpdateAsync(string TokenId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnSubscribeAllRateUpdates", ReplyAction="http://tempuri.org/IRateService/UnSubscribeAllRateUpdatesResponse")]
        void UnSubscribeAllRateUpdates();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnSubscribeAllRateUpdates", ReplyAction="http://tempuri.org/IRateService/UnSubscribeAllRateUpdatesResponse")]
        System.Threading.Tasks.Task UnSubscribeAllRateUpdatesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetAssetList", ReplyAction="http://tempuri.org/IRateService/GetAssetListResponse")]
        string[] GetAssetList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetAssetList", ReplyAction="http://tempuri.org/IRateService/GetAssetListResponse")]
        System.Threading.Tasks.Task<string[]> GetAssetListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetTokenList", ReplyAction="http://tempuri.org/IRateService/GetTokenListResponse")]
        string[] GetTokenList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetTokenList", ReplyAction="http://tempuri.org/IRateService/GetTokenListResponse")]
        System.Threading.Tasks.Task<string[]> GetTokenListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetAssetRateList", ReplyAction="http://tempuri.org/IRateService/GetAssetRateListResponse")]
        AbacasX.Model.DataContracts.AssetRateData[] GetAssetRateList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetAssetRateList", ReplyAction="http://tempuri.org/IRateService/GetAssetRateListResponse")]
        System.Threading.Tasks.Task<AbacasX.Model.DataContracts.AssetRateData[]> GetAssetRateListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetTokenRateList", ReplyAction="http://tempuri.org/IRateService/GetTokenRateListResponse")]
        AbacasX.Model.DataContracts.TokenRateData[] GetTokenRateList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetTokenRateList", ReplyAction="http://tempuri.org/IRateService/GetTokenRateListResponse")]
        System.Threading.Tasks.Task<AbacasX.Model.DataContracts.TokenRateData[]> GetTokenRateListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetTokenPairRate", ReplyAction="http://tempuri.org/IRateService/GetTokenPairRateResponse")]
        AbacasX.Model.DataContracts.TokenPairRateData GetTokenPairRate(string Token1Id, string Token2Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetTokenPairRate", ReplyAction="http://tempuri.org/IRateService/GetTokenPairRateResponse")]
        System.Threading.Tasks.Task<AbacasX.Model.DataContracts.TokenPairRateData> GetTokenPairRateAsync(string Token1Id, string Token2Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetTokenRate", ReplyAction="http://tempuri.org/IRateService/GetTokenRateResponse")]
        AbacasX.Model.DataContracts.TokenRateData GetTokenRate(string Token1Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/GetTokenRate", ReplyAction="http://tempuri.org/IRateService/GetTokenRateResponse")]
        System.Threading.Tasks.Task<AbacasX.Model.DataContracts.TokenRateData> GetTokenRateAsync(string Token1Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/RegisterWithRateManager", ReplyAction="http://tempuri.org/IRateService/RegisterWithRateManagerResponse")]
        void RegisterWithRateManager();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/RegisterWithRateManager", ReplyAction="http://tempuri.org/IRateService/RegisterWithRateManagerResponse")]
        System.Threading.Tasks.Task RegisterWithRateManagerAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnRegisterWithRateManager", ReplyAction="http://tempuri.org/IRateService/UnRegisterWithRateManagerResponse")]
        void UnRegisterWithRateManager();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateService/UnRegisterWithRateManager", ReplyAction="http://tempuri.org/IRateService/UnRegisterWithRateManagerResponse")]
        System.Threading.Tasks.Task UnRegisterWithRateManagerAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRateServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRateService/AssetRateUpdate")]
        void AssetRateUpdate(AbacasX.Model.DataContracts.AssetRateData AssetRateRecord);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRateService/CurrencyPairRateUpdate")]
        void CurrencyPairRateUpdate(AbacasX.Model.DataContracts.CurrencyPairRateData CurrencyPairRateRecord);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRateService/TokenRateUpdate")]
        void TokenRateUpdate(AbacasX.Model.DataContracts.TokenRateData TokenRateRecord);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRateService/TokenPairRateUpdate")]
        void TokenPairRateUpdate(AbacasX.Model.DataContracts.TokenPairRateData TokenPairRateRecord);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRateServiceChannel : AbacasX.Exchange.RateService.IRateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RateServiceClient : System.ServiceModel.DuplexClientBase<AbacasX.Exchange.RateService.IRateService>, AbacasX.Exchange.RateService.IRateService {
        
        public RateServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public RateServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public RateServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RateServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RateServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SubscribeToAssetRateUpdate(string AssetId) {
            base.Channel.SubscribeToAssetRateUpdate(AssetId);
        }
        
        public System.Threading.Tasks.Task SubscribeToAssetRateUpdateAsync(string AssetId) {
            return base.Channel.SubscribeToAssetRateUpdateAsync(AssetId);
        }
        
        public void UnSubscribeToAssetRateUpdate(string AssetId) {
            base.Channel.UnSubscribeToAssetRateUpdate(AssetId);
        }
        
        public System.Threading.Tasks.Task UnSubscribeToAssetRateUpdateAsync(string AssetId) {
            return base.Channel.UnSubscribeToAssetRateUpdateAsync(AssetId);
        }
        
        public void SubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2) {
            base.Channel.SubscribeToCurrencyPairRateUpdate(Currency1, Currency2);
        }
        
        public System.Threading.Tasks.Task SubscribeToCurrencyPairRateUpdateAsync(string Currency1, string Currency2) {
            return base.Channel.SubscribeToCurrencyPairRateUpdateAsync(Currency1, Currency2);
        }
        
        public void UnSubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2) {
            base.Channel.UnSubscribeToCurrencyPairRateUpdate(Currency1, Currency2);
        }
        
        public System.Threading.Tasks.Task UnSubscribeToCurrencyPairRateUpdateAsync(string Currency1, string Currency2) {
            return base.Channel.UnSubscribeToCurrencyPairRateUpdateAsync(Currency1, Currency2);
        }
        
        public void SubscribeToTokenPairRateUpdate(string Token1, string Token2) {
            base.Channel.SubscribeToTokenPairRateUpdate(Token1, Token2);
        }
        
        public System.Threading.Tasks.Task SubscribeToTokenPairRateUpdateAsync(string Token1, string Token2) {
            return base.Channel.SubscribeToTokenPairRateUpdateAsync(Token1, Token2);
        }
        
        public void UnSubscribeToTokenPairRateUpdate(string Token, string Token2) {
            base.Channel.UnSubscribeToTokenPairRateUpdate(Token, Token2);
        }
        
        public System.Threading.Tasks.Task UnSubscribeToTokenPairRateUpdateAsync(string Token, string Token2) {
            return base.Channel.UnSubscribeToTokenPairRateUpdateAsync(Token, Token2);
        }
        
        public void SubscribeToTokenRateUpdate(string TokenId) {
            base.Channel.SubscribeToTokenRateUpdate(TokenId);
        }
        
        public System.Threading.Tasks.Task SubscribeToTokenRateUpdateAsync(string TokenId) {
            return base.Channel.SubscribeToTokenRateUpdateAsync(TokenId);
        }
        
        public void UnSubscribeToTokenRateUpdate(string TokenId) {
            base.Channel.UnSubscribeToTokenRateUpdate(TokenId);
        }
        
        public System.Threading.Tasks.Task UnSubscribeToTokenRateUpdateAsync(string TokenId) {
            return base.Channel.UnSubscribeToTokenRateUpdateAsync(TokenId);
        }
        
        public void UnSubscribeAllRateUpdates() {
            base.Channel.UnSubscribeAllRateUpdates();
        }
        
        public System.Threading.Tasks.Task UnSubscribeAllRateUpdatesAsync() {
            return base.Channel.UnSubscribeAllRateUpdatesAsync();
        }
        
        public string[] GetAssetList() {
            return base.Channel.GetAssetList();
        }
        
        public System.Threading.Tasks.Task<string[]> GetAssetListAsync() {
            return base.Channel.GetAssetListAsync();
        }
        
        public string[] GetTokenList() {
            return base.Channel.GetTokenList();
        }
        
        public System.Threading.Tasks.Task<string[]> GetTokenListAsync() {
            return base.Channel.GetTokenListAsync();
        }
        
        public AbacasX.Model.DataContracts.AssetRateData[] GetAssetRateList() {
            return base.Channel.GetAssetRateList();
        }
        
        public System.Threading.Tasks.Task<AbacasX.Model.DataContracts.AssetRateData[]> GetAssetRateListAsync() {
            return base.Channel.GetAssetRateListAsync();
        }
        
        public AbacasX.Model.DataContracts.TokenRateData[] GetTokenRateList() {
            return base.Channel.GetTokenRateList();
        }
        
        public System.Threading.Tasks.Task<AbacasX.Model.DataContracts.TokenRateData[]> GetTokenRateListAsync() {
            return base.Channel.GetTokenRateListAsync();
        }
        
        public AbacasX.Model.DataContracts.TokenPairRateData GetTokenPairRate(string Token1Id, string Token2Id) {
            return base.Channel.GetTokenPairRate(Token1Id, Token2Id);
        }
        
        public System.Threading.Tasks.Task<AbacasX.Model.DataContracts.TokenPairRateData> GetTokenPairRateAsync(string Token1Id, string Token2Id) {
            return base.Channel.GetTokenPairRateAsync(Token1Id, Token2Id);
        }
        
        public AbacasX.Model.DataContracts.TokenRateData GetTokenRate(string Token1Id) {
            return base.Channel.GetTokenRate(Token1Id);
        }
        
        public System.Threading.Tasks.Task<AbacasX.Model.DataContracts.TokenRateData> GetTokenRateAsync(string Token1Id) {
            return base.Channel.GetTokenRateAsync(Token1Id);
        }
        
        public void RegisterWithRateManager() {
            base.Channel.RegisterWithRateManager();
        }
        
        public System.Threading.Tasks.Task RegisterWithRateManagerAsync() {
            return base.Channel.RegisterWithRateManagerAsync();
        }
        
        public void UnRegisterWithRateManager() {
            base.Channel.UnRegisterWithRateManager();
        }
        
        public System.Threading.Tasks.Task UnRegisterWithRateManagerAsync() {
            return base.Channel.UnRegisterWithRateManagerAsync();
        }
    }
}