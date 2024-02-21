using System;
using System.Collections.Generic;
using System.Text;


namespace YLManager.Field
{
    public class FieldControl
    {
        /// <summary>
        /// 문자 Empty 여부
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool StringEmptyCheck(string field)
        {
            bool check = String.IsNullOrWhiteSpace(field);
            return check;
        }


    }
}
