using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.Security;
using OperatingManagement.Framework.Core;
using OperatingManagement.Framework.Components;
using OperatingManagement.Framework.Storage;
using OperatingManagement.Security;

namespace OperatingManagement.WebKernel.Controls
{
    public class PageMenu:Control
    {
        public PageMenu() { }
        /// <summary>
        /// Gets/sets the name of representing xml file.
        /// </summary>
        public string XmlFileName { get; set; }
        private static readonly string _HTML_MENU = "<li><span>{0}</span><ul>{1}</ul></li>";
        private static readonly string _HTML_ITEM = "<li _c=\"{2}\" id=\"menu-{3}\"><a onfocus=\"javascript:this.blur();\" href=\"{0}\">{1}</a></li>";

        private string RenderMenu()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }
            string fullFileName = GlobalSettings.MapPath(string.Format(
                AspNetConfig.Config["settingPattern"].ToString(), XmlFileName));

            List<CstMenu> menus = Menus.ReadMenu(fullFileName, "Menus/" + XmlFileName);
            AspNetPrincipal principal = (AspNetPrincipal)HttpContext.Current.User;

            StringBuilder sbMenu = new StringBuilder();
            bool shouldBeDisplayed = false;
            StringBuilder sbItem = new StringBuilder();
            int totalCount = 0, currentCount = 0;
            foreach (CstMenu m in menus)
            {
                shouldBeDisplayed = false;
                sbItem.Clear();
                currentCount = 1;
                foreach (CstMenuItem item in m.MenuItems)
                {
                    if (item.Roles == "Anyone" || principal.IsInRole(item.Roles))
                    {
                        shouldBeDisplayed = true;
                        currentCount++;
                        sbItem.AppendFormat(_HTML_ITEM,
                            (item.Href.StartsWith("javascript:") ?
                                item.Href : (GlobalSettings.RelativeWebRoot + item.Href)),
                            item.Title,
                            totalCount + currentCount,
                            item.Id);
                    }
                }
                if (shouldBeDisplayed)
                {
                    sbMenu.AppendFormat(_HTML_MENU, m.Title, sbItem.ToString());
                    totalCount += currentCount;
                }
            }
            return "<ul>" + sbMenu.ToString() + "</ul>";
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            string html = this.RenderMenu();
            if (!string.IsNullOrEmpty(html))
            {
                writer.Write(html);
                writer.WriteLine(Environment.NewLine);
            }
        }
    }
}
