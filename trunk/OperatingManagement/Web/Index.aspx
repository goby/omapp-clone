<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="OperatingManagement.Web.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrap">
        <div class="head">
            <div class="logo">&nbsp;</div>
            <ul class="menu-top">
                <li class="selected">首&nbsp;&nbsp;页</li>
                <li class="split">&nbsp;</li>
                <li>用户角色</li>
                <li class="split">&nbsp;</li>
                <li>计划管理</li>
                <li class="split">&nbsp;</li>
                <li>运行管理</li>
                <li class="split">&nbsp;</li>
                <li>业务管理</li>
            </ul>
            <br class="clear" />
            <div class="navigator">
                <div class="nav nav-c1">
                    <div class="nav nav-c2">
                        <div class="nav nav-c3">
                            欢迎您的访问，系统管理员！
                        </div>                    
                    </div>
                </div>
            </div>

            <ul class="styleSelector">
                <li><span class="red">&nbsp;</span></li>
                <li><span class="green">&nbsp;</span></li>
                <li><span class="blue">&nbsp;</span></li>
                <li><span class="brown">&nbsp;</span></li>
                <li><span class="yellow">&nbsp;</span></li>
            </ul>
            <br class="clear" />
        </div>
        <div class="body">
            <div class="left">
                <div class="title">&nbsp;</div>
                <div class="content">
                    <om:PageMenu runat="Server" XmlFileName="menuIndex" />
                </div>
                <div class="bot">&nbsp;</div>
            </div>
            <div class="right">
                <div class="title">
                    <div>
                        <span>首页 &gt; 分系统监控</span>
                    </div>
                </div>
                <div class="content">
                    <div class="ctnx">
                        
                    </div>
                    <%--<div id="leftMenuIndicator">分系统监控</div>--%>
                </div>
                <div class="bot"><span>&nbsp;</span></div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
