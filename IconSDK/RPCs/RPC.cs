using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IconSDK.RPCs
{
    using Extensions;
    using Types;

    public class RPC<TRPCRequestMessage, TRPCResponseMessage>
        where TRPCRequestMessage : RPCRequestMessage
        where TRPCResponseMessage : RPCResponseMessage
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new JsonConverter[]
            {
                new BigIntegerConverter(),
                new DictionaryConverter(),
                new BoolConverter(),
                new BoolNullableConverter(),
                new BytesConverter<Bytes>(),
                new BytesConverter<Hash32>(),
                new BytesConverter<ExternalAddress>(),
                new BytesConverter<ContractAddress>(),
                new BytesConverter<Signature>(),
            }
        };

        public static Func<TRPCRequestMessage, Task<TRPCResponseMessage>> Create(string url)
        {
            return new RPC<TRPCRequestMessage, TRPCResponseMessage>(url).Invoke;
        }

        public readonly string URL;

        public RPC(string url)
        {
            URL = url;
        }

        public async Task<TRPCResponseMessage> Invoke(TRPCRequestMessage requestMessage)
        {
            using (var httpClient = new HttpClient())
            {
                string message = JsonConvert.SerializeObject(requestMessage, _settings);
                using (var result = await httpClient.PostAsync(
                    URL,
                    new StringContent(
                        message,
                        Encoding.UTF8,
                        "application/json"
                    )
                ))
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<TRPCResponseMessage>(resultContent, _settings);
                    if (!responseMessage.IsSuccess)
                        throw RPCException.Create(responseMessage.Error.Code, responseMessage.Error.Message);
                    return responseMessage;
                }
            }
        }
    }
}