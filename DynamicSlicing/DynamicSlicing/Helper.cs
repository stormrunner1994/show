using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSlicing
{
    class Helper
    {
        public static string ListToString(List<int> list)
        {
            string output = "";

            foreach (int i in list)
            {
                if (output != "") output += "," + i;
                else output = i.ToString();
            }
            return output;
        }

        public static void Append(ref string s1, string s2, string trenner)
        {
            if (s1 == "")
                s1 = s2;
            else
                s1 += trenner + s2;
        }

        public static string ListToString(List<string> list)
        {
            string output = "";

            foreach (string s in list)
                Helper.Append(ref output, s, "\n");

            return output;
        }
        public static string DictToString(Dictionary<string, int> dict)
        {
            string output = "";

            foreach (KeyValuePair<string, int> pair in dict)
                Helper.Append(ref output, pair.Key + " = " + pair.Value, ",");

            return output;
        }
    }
}
