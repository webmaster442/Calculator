﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MnbCurrencyRates
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetCurrenciesRequestBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetCurrenciesRequestBody : object
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetCurrenciesResponseBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetCurrenciesResponseBody : object
    {
        
        private string GetCurrenciesResultField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        internal string GetCurrenciesResult
        {
            get
            {
                return this.GetCurrenciesResultField;
            }
            set
            {
                this.GetCurrenciesResultField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetCurrencyUnitsRequestBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetCurrencyUnitsRequestBody : object
    {
        
        private string currencyNamesField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        internal string currencyNames
        {
            get
            {
                return this.currencyNamesField;
            }
            set
            {
                this.currencyNamesField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetCurrencyUnitsResponseBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetCurrencyUnitsResponseBody : object
    {
        
        private string GetCurrencyUnitsResultField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        internal string GetCurrencyUnitsResult
        {
            get
            {
                return this.GetCurrencyUnitsResultField;
            }
            set
            {
                this.GetCurrencyUnitsResultField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetCurrentExchangeRatesRequestBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetCurrentExchangeRatesRequestBody : object
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetCurrentExchangeRatesResponseBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetCurrentExchangeRatesResponseBody : object
    {
        
        private string GetCurrentExchangeRatesResultField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        internal string GetCurrentExchangeRatesResult
        {
            get
            {
                return this.GetCurrentExchangeRatesResultField;
            }
            set
            {
                this.GetCurrentExchangeRatesResultField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetDateIntervalRequestBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetDateIntervalRequestBody : object
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetDateIntervalResponseBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetDateIntervalResponseBody : object
    {
        
        private string GetDateIntervalResultField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        internal string GetDateIntervalResult
        {
            get
            {
                return this.GetDateIntervalResultField;
            }
            set
            {
                this.GetDateIntervalResultField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetExchangeRatesRequestBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetExchangeRatesRequestBody : object
    {
        
        private string startDateField;
        
        private string endDateField;
        
        private string currencyNamesField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        internal string startDate
        {
            get
            {
                return this.startDateField;
            }
            set
            {
                this.startDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        internal string endDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        internal string currencyNames
        {
            get
            {
                return this.currencyNamesField;
            }
            set
            {
                this.currencyNamesField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetExchangeRatesResponseBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetExchangeRatesResponseBody : object
    {
        
        private string GetExchangeRatesResultField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        internal string GetExchangeRatesResult
        {
            get
            {
                return this.GetExchangeRatesResultField;
            }
            set
            {
                this.GetExchangeRatesResultField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetInfoRequestBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetInfoRequestBody : object
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetInfoResponseBody", Namespace="http://www.mnb.hu/webservices/")]
    internal partial class GetInfoResponseBody : object
    {
        
        private string GetInfoResultField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        internal string GetInfoResult
        {
            get
            {
                return this.GetInfoResultField;
            }
            set
            {
                this.GetInfoResultField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.mnb.hu/webservices/", ConfigurationName="MnbCurrencyRates.MNBArfolyamServiceSoap")]
    internal interface MNBArfolyamServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencies", ReplyAction="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrenciesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(string), Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrenciesStringFault", Name="string", Namespace="http://schemas.microsoft.com/2003/10/Serialization/")]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetCurrenciesResponse> GetCurrenciesAsync(MnbCurrencyRates.GetCurrenciesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencyUnits", ReplyAction="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencyUnitsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(string), Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencyUnitsStringFault", Name="string", Namespace="http://schemas.microsoft.com/2003/10/Serialization/")]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetCurrencyUnitsResponse> GetCurrencyUnitsAsync(MnbCurrencyRates.GetCurrencyUnitsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrentExchangeRates", ReplyAction="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrentExchangeRatesRespo" +
            "nse")]
        [System.ServiceModel.FaultContractAttribute(typeof(string), Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrentExchangeRatesStrin" +
            "gFault", Name="string", Namespace="http://schemas.microsoft.com/2003/10/Serialization/")]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetCurrentExchangeRatesResponse> GetCurrentExchangeRatesAsync(MnbCurrencyRates.GetCurrentExchangeRatesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetDateInterval", ReplyAction="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetDateIntervalResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(string), Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetDateIntervalStringFault", Name="string", Namespace="http://schemas.microsoft.com/2003/10/Serialization/")]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetDateIntervalResponse> GetDateIntervalAsync(MnbCurrencyRates.GetDateIntervalRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetExchangeRates", ReplyAction="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetExchangeRatesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(string), Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetExchangeRatesStringFault", Name="string", Namespace="http://schemas.microsoft.com/2003/10/Serialization/")]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetExchangeRatesResponse> GetExchangeRatesAsync(MnbCurrencyRates.GetExchangeRatesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetInfo", ReplyAction="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(string), Action="http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetInfoStringFault", Name="string", Namespace="http://schemas.microsoft.com/2003/10/Serialization/")]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetInfoResponse> GetInfoAsync(MnbCurrencyRates.GetInfoRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetCurrenciesRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetCurrenciesRequestBody GetCurrencies;
        
        public GetCurrenciesRequest()
        {
        }
        
        public GetCurrenciesRequest(MnbCurrencyRates.GetCurrenciesRequestBody GetCurrencies)
        {
            this.GetCurrencies = GetCurrencies;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetCurrenciesResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCurrenciesResponse", Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetCurrenciesResponseBody GetCurrenciesResponse1;
        
        public GetCurrenciesResponse()
        {
        }
        
        public GetCurrenciesResponse(MnbCurrencyRates.GetCurrenciesResponseBody GetCurrenciesResponse1)
        {
            this.GetCurrenciesResponse1 = GetCurrenciesResponse1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetCurrencyUnitsRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetCurrencyUnitsRequestBody GetCurrencyUnits;
        
        public GetCurrencyUnitsRequest()
        {
        }
        
        public GetCurrencyUnitsRequest(MnbCurrencyRates.GetCurrencyUnitsRequestBody GetCurrencyUnits)
        {
            this.GetCurrencyUnits = GetCurrencyUnits;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetCurrencyUnitsResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCurrencyUnitsResponse", Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetCurrencyUnitsResponseBody GetCurrencyUnitsResponse1;
        
        public GetCurrencyUnitsResponse()
        {
        }
        
        public GetCurrencyUnitsResponse(MnbCurrencyRates.GetCurrencyUnitsResponseBody GetCurrencyUnitsResponse1)
        {
            this.GetCurrencyUnitsResponse1 = GetCurrencyUnitsResponse1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetCurrentExchangeRatesRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetCurrentExchangeRatesRequestBody GetCurrentExchangeRates;
        
        public GetCurrentExchangeRatesRequest()
        {
        }
        
        public GetCurrentExchangeRatesRequest(MnbCurrencyRates.GetCurrentExchangeRatesRequestBody GetCurrentExchangeRates)
        {
            this.GetCurrentExchangeRates = GetCurrentExchangeRates;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetCurrentExchangeRatesResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCurrentExchangeRatesResponse", Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetCurrentExchangeRatesResponseBody GetCurrentExchangeRatesResponse1;
        
        public GetCurrentExchangeRatesResponse()
        {
        }
        
        public GetCurrentExchangeRatesResponse(MnbCurrencyRates.GetCurrentExchangeRatesResponseBody GetCurrentExchangeRatesResponse1)
        {
            this.GetCurrentExchangeRatesResponse1 = GetCurrentExchangeRatesResponse1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetDateIntervalRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetDateIntervalRequestBody GetDateInterval;
        
        public GetDateIntervalRequest()
        {
        }
        
        public GetDateIntervalRequest(MnbCurrencyRates.GetDateIntervalRequestBody GetDateInterval)
        {
            this.GetDateInterval = GetDateInterval;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetDateIntervalResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDateIntervalResponse", Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetDateIntervalResponseBody GetDateIntervalResponse1;
        
        public GetDateIntervalResponse()
        {
        }
        
        public GetDateIntervalResponse(MnbCurrencyRates.GetDateIntervalResponseBody GetDateIntervalResponse1)
        {
            this.GetDateIntervalResponse1 = GetDateIntervalResponse1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetExchangeRatesRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetExchangeRatesRequestBody GetExchangeRates;
        
        public GetExchangeRatesRequest()
        {
        }
        
        public GetExchangeRatesRequest(MnbCurrencyRates.GetExchangeRatesRequestBody GetExchangeRates)
        {
            this.GetExchangeRates = GetExchangeRates;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetExchangeRatesResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetExchangeRatesResponse", Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetExchangeRatesResponseBody GetExchangeRatesResponse1;
        
        public GetExchangeRatesResponse()
        {
        }
        
        public GetExchangeRatesResponse(MnbCurrencyRates.GetExchangeRatesResponseBody GetExchangeRatesResponse1)
        {
            this.GetExchangeRatesResponse1 = GetExchangeRatesResponse1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetInfoRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetInfoRequestBody GetInfo;
        
        public GetInfoRequest()
        {
        }
        
        public GetInfoRequest(MnbCurrencyRates.GetInfoRequestBody GetInfo)
        {
            this.GetInfo = GetInfo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class GetInfoResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetInfoResponse", Namespace="http://www.mnb.hu/webservices/", Order=0)]
        public MnbCurrencyRates.GetInfoResponseBody GetInfoResponse1;
        
        public GetInfoResponse()
        {
        }
        
        public GetInfoResponse(MnbCurrencyRates.GetInfoResponseBody GetInfoResponse1)
        {
            this.GetInfoResponse1 = GetInfoResponse1;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    internal interface MNBArfolyamServiceSoapChannel : MnbCurrencyRates.MNBArfolyamServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    internal partial class MNBArfolyamServiceSoapClient : System.ServiceModel.ClientBase<MnbCurrencyRates.MNBArfolyamServiceSoap>, MnbCurrencyRates.MNBArfolyamServiceSoap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public MNBArfolyamServiceSoapClient() : 
                base(MNBArfolyamServiceSoapClient.GetDefaultBinding(), MNBArfolyamServiceSoapClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.CustomBinding_MNBArfolyamServiceSoap.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MNBArfolyamServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(MNBArfolyamServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), MNBArfolyamServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MNBArfolyamServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(MNBArfolyamServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MNBArfolyamServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(MNBArfolyamServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MNBArfolyamServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetCurrenciesResponse> MnbCurrencyRates.MNBArfolyamServiceSoap.GetCurrenciesAsync(MnbCurrencyRates.GetCurrenciesRequest request)
        {
            return base.Channel.GetCurrenciesAsync(request);
        }
        
        public System.Threading.Tasks.Task<MnbCurrencyRates.GetCurrenciesResponse> GetCurrenciesAsync(MnbCurrencyRates.GetCurrenciesRequestBody GetCurrencies)
        {
            MnbCurrencyRates.GetCurrenciesRequest inValue = new MnbCurrencyRates.GetCurrenciesRequest();
            inValue.GetCurrencies = GetCurrencies;
            return ((MnbCurrencyRates.MNBArfolyamServiceSoap)(this)).GetCurrenciesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetCurrencyUnitsResponse> MnbCurrencyRates.MNBArfolyamServiceSoap.GetCurrencyUnitsAsync(MnbCurrencyRates.GetCurrencyUnitsRequest request)
        {
            return base.Channel.GetCurrencyUnitsAsync(request);
        }
        
        public System.Threading.Tasks.Task<MnbCurrencyRates.GetCurrencyUnitsResponse> GetCurrencyUnitsAsync(MnbCurrencyRates.GetCurrencyUnitsRequestBody GetCurrencyUnits)
        {
            MnbCurrencyRates.GetCurrencyUnitsRequest inValue = new MnbCurrencyRates.GetCurrencyUnitsRequest();
            inValue.GetCurrencyUnits = GetCurrencyUnits;
            return ((MnbCurrencyRates.MNBArfolyamServiceSoap)(this)).GetCurrencyUnitsAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetCurrentExchangeRatesResponse> MnbCurrencyRates.MNBArfolyamServiceSoap.GetCurrentExchangeRatesAsync(MnbCurrencyRates.GetCurrentExchangeRatesRequest request)
        {
            return base.Channel.GetCurrentExchangeRatesAsync(request);
        }
        
        public System.Threading.Tasks.Task<MnbCurrencyRates.GetCurrentExchangeRatesResponse> GetCurrentExchangeRatesAsync(MnbCurrencyRates.GetCurrentExchangeRatesRequestBody GetCurrentExchangeRates)
        {
            MnbCurrencyRates.GetCurrentExchangeRatesRequest inValue = new MnbCurrencyRates.GetCurrentExchangeRatesRequest();
            inValue.GetCurrentExchangeRates = GetCurrentExchangeRates;
            return ((MnbCurrencyRates.MNBArfolyamServiceSoap)(this)).GetCurrentExchangeRatesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetDateIntervalResponse> MnbCurrencyRates.MNBArfolyamServiceSoap.GetDateIntervalAsync(MnbCurrencyRates.GetDateIntervalRequest request)
        {
            return base.Channel.GetDateIntervalAsync(request);
        }
        
        public System.Threading.Tasks.Task<MnbCurrencyRates.GetDateIntervalResponse> GetDateIntervalAsync(MnbCurrencyRates.GetDateIntervalRequestBody GetDateInterval)
        {
            MnbCurrencyRates.GetDateIntervalRequest inValue = new MnbCurrencyRates.GetDateIntervalRequest();
            inValue.GetDateInterval = GetDateInterval;
            return ((MnbCurrencyRates.MNBArfolyamServiceSoap)(this)).GetDateIntervalAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetExchangeRatesResponse> MnbCurrencyRates.MNBArfolyamServiceSoap.GetExchangeRatesAsync(MnbCurrencyRates.GetExchangeRatesRequest request)
        {
            return base.Channel.GetExchangeRatesAsync(request);
        }
        
        public System.Threading.Tasks.Task<MnbCurrencyRates.GetExchangeRatesResponse> GetExchangeRatesAsync(MnbCurrencyRates.GetExchangeRatesRequestBody GetExchangeRates)
        {
            MnbCurrencyRates.GetExchangeRatesRequest inValue = new MnbCurrencyRates.GetExchangeRatesRequest();
            inValue.GetExchangeRates = GetExchangeRates;
            return ((MnbCurrencyRates.MNBArfolyamServiceSoap)(this)).GetExchangeRatesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MnbCurrencyRates.GetInfoResponse> MnbCurrencyRates.MNBArfolyamServiceSoap.GetInfoAsync(MnbCurrencyRates.GetInfoRequest request)
        {
            return base.Channel.GetInfoAsync(request);
        }
        
        public System.Threading.Tasks.Task<MnbCurrencyRates.GetInfoResponse> GetInfoAsync(MnbCurrencyRates.GetInfoRequestBody GetInfo)
        {
            MnbCurrencyRates.GetInfoRequest inValue = new MnbCurrencyRates.GetInfoRequest();
            inValue.GetInfo = GetInfo;
            return ((MnbCurrencyRates.MNBArfolyamServiceSoap)(this)).GetInfoAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.CustomBinding_MNBArfolyamServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.CustomBinding_MNBArfolyamServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://www.mnb.hu/arfolyamok.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return MNBArfolyamServiceSoapClient.GetBindingForEndpoint(EndpointConfiguration.CustomBinding_MNBArfolyamServiceSoap);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return MNBArfolyamServiceSoapClient.GetEndpointAddress(EndpointConfiguration.CustomBinding_MNBArfolyamServiceSoap);
        }
        
        public enum EndpointConfiguration
        {
            
            CustomBinding_MNBArfolyamServiceSoap,
        }
    }
}