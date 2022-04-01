using SmartStore.Collections;
using SmartStore.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.CreditCardPay
{
    public class AdminMenu : AdminMenuProvider
    {
        protected override void BuildMenuCore(TreeNode<MenuItem> pluginsNode)
        {
            var customerNode = pluginsNode.Parent.SelectNode(x => x.Value.Id == "users");
            var menuItem = new MenuItem().ToBuilder()
                .Text("Credit Cards")
                .ResKey("Plugins.FriendlyName.SmartStore.CreditCardPay")
                .Icon("fal fa-fw fa-xl fa-credit-card")
                .Action("LoadCustomers", "CustomerCreditCard", new { area = "SmartStore.CreditCardPay" })
                .ToItem();

            customerNode.Prepend(menuItem);
        }

        public override int Ordinal => 100;
    }
}