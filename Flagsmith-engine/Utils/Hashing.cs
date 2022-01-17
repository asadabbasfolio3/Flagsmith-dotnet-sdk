using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Flagsmith_engine.Utils
{
    internal static class Hashing
    {
        public static float GetHashedPercentageForObjectIds(List<string> objectIds, int iteration = 1)
        {
            var toHash = String.Join(",", repeatIdsList(objectIds, iteration));
            var hashedValueAsInt = Convert.ToInt32(CreateMD5(toHash), 16);
            var value = ((hashedValueAsInt % 9999) / 9998) * 100;
            return value == 100 ? GetHashedPercentageForObjectIds(objectIds, ++iteration) : value;
        }
        private static List<string> repeatIdsList(List<string> objectIds, int iteration)
        {
            var list = new List<string>();
            foreach (var _ in Enumerable.Range(1, iteration))
            {
                list.AddRange(objectIds);
            }
            return list;
        }
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
