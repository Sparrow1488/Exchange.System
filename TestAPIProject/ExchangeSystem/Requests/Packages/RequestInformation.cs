﻿using ExchangeSystem.SecurityData;
using Newtonsoft.Json;

namespace ExchangeSystem.Requests.Packages
{
    public class RequestInformation
    {
        public RequestInformation(EncryptTypes type, int dataSize)
        {
            EncryptType = type;
            DataSize = dataSize;
        }
        [JsonProperty("type")]
        public EncryptTypes EncryptType { get; }
        public int DataSize { get; }
    }
}
