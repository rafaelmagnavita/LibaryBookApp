using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDomain.Enums
{
    public static class SearchEnums
    {
        public enum BookSearchCommand        
        {
            [Description("By Title")]
            TITLE = 1,
            [Description("By ISBN")]
            ISBN, 
            [Description("By Id")]
            ID
        }
    }
}
