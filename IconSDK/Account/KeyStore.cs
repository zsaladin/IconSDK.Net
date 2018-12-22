using System;
using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nethereum.KeyStore;

namespace IconSDK.Account
{
    using Types;

    public class KeyStore
    {
        private static KeyStoreService _keyStoreService = new KeyStoreService();

        public readonly PrivateKey PrivateKey;
        public readonly ExternalAddress Address;

        private KeyStore(PrivateKey privateKey, ExternalAddress address)
        {
            PrivateKey = privateKey;
            Address = address;
        }

        public static KeyStore Create(PrivateKey privateKey, ExternalAddress address)
        {
            return new KeyStore(privateKey, address);
        }

        public static KeyStore Load(string password, string filePath)
        {
            string json;
            using (var file = File.OpenText(filePath))
                json = file.ReadToEnd();

            var privateKey = _keyStoreService.DecryptKeyStoreFromJson(password, json);
            var address = _keyStoreService.GetAddressFromKeyStore(json);

            return new KeyStore(new PrivateKey(privateKey), new ExternalAddress(address));
        }

        public string Store(string password, string filePath = null)
        {
            var json = _keyStoreService.EncryptAndGenerateDefaultKeyStoreAsJson(
                password,
                PrivateKey.Binary.ToArray(),
                Address.ToString()
            );

            JToken token = JObject.Parse(json);
            token["coinType"] = "icx";

            if (filePath == null)
                filePath = _keyStoreService.GenerateUTCFileName(Address.ToString());

            File.WriteAllText(filePath, token.ToString(Formatting.None));
            return filePath;
        }
    }
}
