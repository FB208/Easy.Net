using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace EasyDotNet.MVC.Sample.EnumCodes
{
    public enum ApproveEnum
    {
        [Description(description:"提交")]
        Submit=0,
        [Description(description: "通过")]
        Pass =1,
        [Description(description: "退回")]
        Untread =2
    }
}