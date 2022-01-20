using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
namespace Flagsmith_engine.Utils
{
    internal static class Hashing
    {
        public static float GetHashedPercentageForObjectIds(List<string> objectIds, int iteration = 1)
        {
            var toHash = String.Join(",", repeatIdsList(objectIds, iteration));
            var hashedValueAsInt = CreateMD5AsInt(toHash);
            var value = ((float)(hashedValueAsInt % 9999) / 9998) * 100;
            return value == 100 ? GetHashedPercentageForObjectIds(objectIds, ++iteration) : value;
        }
        private static List<string> repeatIdsList(List<string> objectIds, int iteration)
        {
            var list = new List<string>();
            foreach (var _ in Enumerable.Range(0, iteration + 1))
            {
                list.AddRange(objectIds);
            }
            return list;
        }
        public static BigInteger CreateMD5AsInt(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }
                return new BigInteger(Encoding.UTF8.GetBytes(sb.ToString()));
            }
        }
    }
}
