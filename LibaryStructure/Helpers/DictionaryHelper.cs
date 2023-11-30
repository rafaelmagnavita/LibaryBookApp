using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.Helpers
{
    public static class DictionaryHelper
    {
        private static List<string> _keys;
        private static List<object> _values;
        public static void Start()
        {
            _keys = new List<string>();
            _values = new List<object>();
        }
        public static void SetParam(string key, object value, bool newDic = false)
        {
            if (newDic)
                Start();
            _keys.Append(key);
            _values.Append(value);
        }
        public static Dictionary<string, object> GetDictionary() {
            var dict = new Dictionary<string, object>();
            for (int i = 0; i < _keys.Count(); i++)
            {
                dict.Add(_keys[i], _values[i]);
            }
            return dict;
        }        
    }
}
