using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExchangeServer.LocalDataBase
{
    public static class ServerLocalDb
    {
        /// <summary>
        /// Key - пасспорт пользователя; Value - его токен
        /// </summary>
        public static Dictionary<UserPassport, string> AuthTokens { get; } = new Dictionary<UserPassport, string>();
        /// <summary>
        /// Поиск id авторизованного пользователя в системе, используя токен
        /// </summary>
        public static bool WasAuth(string token)
        {
            var findPairs = FindBy(token);
            if(findPairs.Length > 0)
            {
                foreach (var pair in findPairs)
                {
                    if (pair.Value == token)
                        return true;
                }
            }
            return false;
        }
        public static AdminStatus CheckStatus(string token)
        {
            var findPassport = FindPassportBy(token);
            if (findPassport == null)
                return AdminStatus.User;
            return findPassport.AdminStatus;
        }
        private static KeyValuePair<UserPassport, string>[] FindBy(string token)
        {

            var findPairs = AuthTokens.Where(pair => pair.Value == token).ToArray();
            return findPairs;
        }
        /// <summary>
        /// Ищет в AuthTokens пасспорт авторизованного по токену пользователя
        /// </summary>
        /// <returns>Null, если не найдет</returns>
        public static UserPassport FindPassportBy(string token)
        {
            var findPairs = FindBy(token);
            if (findPairs.Length > 0)
            {
                foreach (var pair in findPairs)
                {
                    if (pair.Value == token)
                        return pair.Key;
                }
            }
            return null;
        }
        /// <summary>
        /// Добавляет в базу авторизованного пользователя
        /// </summary>
        /// <returns>Токен авторизации</returns>
        /// <exception cref="ArgumentException">Не валидный id</exception>
        public static string AddNew(UserPassport authUser) 
        {
            if (authUser.Id < 0)
                throw new ArgumentException("Id пользователя не может быть меньше нуля!");
            var uniqueToken = GenerateToken();
            AuthTokens.Add(authUser, uniqueToken);
            return uniqueToken;
        }
        private static string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }
    }
}
