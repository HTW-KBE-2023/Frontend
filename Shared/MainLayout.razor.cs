using BootstrapBlazor.Components;

using Microsoft.AspNetCore.Components.Routing;

namespace Frontend.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout
    {
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;

        private List<MenuItem>? Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Menus = GetIconSideMenuItems();
        }

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus = new List<MenuItem>
        {
            new MenuItem() { Text = "Startseite", Icon = "fa-solid fa-fw fa-house", Url = "/" , Match = NavLinkMatch.All},
            new MenuItem() { Text = "RPG-Bot", Icon = "fa-solid fa-fw fa-dragon", Url = "/rpg" },
            new MenuItem() { Text = "Umfrage", Icon = "fa-solid fa-fw fa-square-poll-vertical", Url = "/poll" },
        };

            return menus;
        }
    }
}