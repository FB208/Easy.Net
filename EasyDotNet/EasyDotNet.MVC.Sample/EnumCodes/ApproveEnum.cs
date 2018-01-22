using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using EasyDotNet.Utility;

namespace EasyDotNet.MVC.Sample.EnumCodes
{
    public enum ApproveEnum
    {
        [EnumHelper.HtmlElementAttributes(Format = "<span title=\"{1}\" class=\"label label-info labsite\">{0}</span>")]
        [Description(description:"提交")]
        Submit=0,
        [EnumHelper.HtmlElementAttributes(Format = "<span title=\"{1}\" class=\"label label-success labsite\">{0}</span>")]
        [Description(description: "通过")]
        Pass =1,
        [EnumHelper.HtmlElementAttributes(Format = "<span title=\"{1}\" class=\"label label-danger labsite\">{0}</span>")]
        [Description(description: "退回")]
        Untread =2
    }
}