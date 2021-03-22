using System;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class EntityMapper
    {
        protected string GetStringValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is string)
                return (string) val;

            return "";
        }

        protected int GetIntValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName))
            {
                if (val is int || val is decimal || val is char) return (int) val;
                if (val is short) return (short) val;
            }

            return -1;
        }

        protected double GetDoubleValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is double)
                return (double) dic[attName];

            return -1;
        }

        protected decimal GetDecimalValue(Dictionary<string, object> dic, string keyName)
        {
            var val = dic[keyName];
            if (dic.ContainsKey(keyName) && val is decimal)
            {
                return (decimal) val;
            }

            return -1;
        }

        protected DateTime GetDateValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is DateTime)
                return (DateTime) dic[attName];

            return DateTime.Now;
        }
    }
}