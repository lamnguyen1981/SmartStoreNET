using SmartStore.Collections;
using SmartStore.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Zoho
{
    public class AdminMenu : AdminMenuProvider
    {
        protected override void BuildMenuCore(TreeNode<MenuItem> pluginsNode)
        {
            var customerNode = pluginsNode.Parent.SelectNode(x => x.Value.Id == "sales");
            var menuItem = new MenuItem().ToBuilder()
                .Text("Zoho")
                .ResKey("Plugins.FriendlyName.CC.Plugins.Zoho")
                .Icon("fas fa-file-invoice-dollar")
                .Action("GetInvoices", "Zoho", new { area = "CC.Plugins.Zoho" })
                .ToItem();

            customerNode.Prepend(menuItem);
        }

        public override int Ordinal => 100;
    }
}