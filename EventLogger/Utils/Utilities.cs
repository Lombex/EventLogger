using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using System.Reflection;

namespace Utils
{
    public static class Utilities
    {
        public static HarmonyMethod GetPatch(this Type Type, string Name, bool PublicMethod = false)
        {
            return new HarmonyMethod(Type.GetMethod(Name, (PublicMethod ? BindingFlags.Public : BindingFlags.NonPublic) | BindingFlags.Static));
        }
        public static string PrintBytes(this byte[] byteArray)
        {
            var sb = new StringBuilder("");
            for (var i = 0; i < byteArray.Length; i++)
            {
                var b = byteArray[i];
                sb.Append(b);
                if (i < byteArray.Length - 1) sb.Append(", ");
            }
            sb.Append("");
            return sb.ToString();
        }
    }
}
