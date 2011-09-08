<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OperatingManagement.Web.Account.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="box login">
        <div class="b-r1c1">
            <div class="b-r1c2">
                <div class="b-r1c3">
                    &nbsp;</div>
            </div>
        </div>
        <div class="b-r2c1">
            <div class="b-r2c2">
                <div class="b-r2c3">
                    <div class="l-c-r1">
                        &nbsp;
                    </div>
                    <hr />
                    <div class="l-c-r2">
                        <div>
                            <b>用户名 </b>
                        </div>
                        <div>
                            <input type="text" class="text" style="width: 300px;" />
                        </div>
                        <div>
                            <b>密码 </b>
                        </div>
                        <div>
                            <input type="password" class="text" style="width: 300px;" />
                        </div>
                        <div>
                            <button class="button">
                                登录</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="b-r3c1">
            <div class="b-r3c2">
                <div class="b-r3c3 copyright">
                    CopyRight &copy; <%= this.CopyRight %> 2011</div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
