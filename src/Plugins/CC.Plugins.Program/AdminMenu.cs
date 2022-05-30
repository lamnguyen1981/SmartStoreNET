using SmartStore.Collections;
using SmartStore.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Program
{
    public class AdminMenu : AdminMenuProvider
    {
        protected override void BuildMenuCore(TreeNode<MenuItem> pluginsNode)
        {
            var customerNode = pluginsNode.Parent.SelectNode(x => x.Value.Id == "catalog");
            var menuItem = new MenuItem().ToBuilder()
                .Text("Program")
                .ResKey("Plugins.FriendlyName.CC.Plugins.Program")
                .Icon("fa fa-map-marker")
                .Action("Index", "Program", new { area = "CC.Plugins.Program" })
                .ToItem();

            customerNode.Prepend(menuItem);
        }

        public override int Ordinal => 100;
    }
}