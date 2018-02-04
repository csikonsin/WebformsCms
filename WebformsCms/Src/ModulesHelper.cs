using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WebformsCms.Src;

namespace WebformsCms.Src
{
    public static class ModulesHelper
    {
        public static IEnumerable<Control> FlattenChildren(this Control control)
        {
            var children = control.Controls.Cast<Control>();
            return children.SelectMany(c => FlattenChildren(c)).Concat(children);
        }
    }
}