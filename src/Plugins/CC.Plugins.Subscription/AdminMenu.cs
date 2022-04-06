using SmartStore.Collections;
using SmartStore.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription
{
    public class AdminMenu : AdminMenuProvider
    {
        protected override void BuildMenuCore(TreeNode<MenuItem> pluginsNode)
        {
            var customerNode = pluginsNode.Parent.SelectNode(x => x.Value.Id == "users");
            var menuItem = new MenuItem().ToBuilder()
                .Text("Credit Cards")
                .ResKey("Plugins.FriendlyName.CC.Plugins.Subscription")
                .Icon("fal fa-fw fa-xl fa-credit-card")
                .Action("LoadCustomers", "CustomerSubscription", new { area = "CC.Plugins.Subscription" })
                .ToItem();

            customerNode.Prepend(menuItem);
        }

        public override int Ordinal => 100;
    }
}