using Xenko.Core.Mathematics;
using Xenko.DebugDrawer;
using Xenko.Engine;
using Xenko.Graphics;
using Xenko.UI;
using Xenko.UI.Controls;
using Xenko.UI.Panels;

namespace DebugDrawer
{
    public class SceneSelectorComponent : StartupScript
    {
        private SpriteFont _font;
        private MenuItems[] _menuItems;
        private StackPanel _menuPanel;
        private UIPage _page;

        public override void Start()
        {
            base.Start();

            _page = Entity.Get<UIComponent>().Page;
            _menuPanel = _page.RootElement.FindName("SceneMenu") as StackPanel;
            _font = Content.Load<SpriteFont>("Sansation");
            _menuItems = new[]
            {
                new MenuItems("Simple Shapes", "Examples/SimpleShapes", true),
                new MenuItems("Color Change", "Examples/ColorChange", false)
            };

            foreach (var menuItem in _menuItems) _menuPanel.Children.Add(CreateMenuItem(menuItem));
        }

        private UIElement CreateMenuItem(MenuItems menuItem)
        {
            var border = new Border
            {
                CanBeHitByUser = true,
                BorderThickness = new Thickness(1, 1, 1, 1),
                BorderColor = Color.Zero,
                Content = new TextBlock
                {
                    Text = menuItem.Name,
                    Font = _font
                }
            };

            border.MouseOverStateChanged += (sender, args) =>
            {
                var isMouseOver = args.NewValue == MouseOverState.MouseOverChild ||
                                  args.NewValue == MouseOverState.MouseOverElement;
                border.BorderColor = isMouseOver ? Color.Aquamarine : Color.Zero;
            };

            border.TouchUp += SelectScene;

            return border;
        }

        private void SelectScene(object sender, TouchEventArgs e)
        {
            var name = ((e.Source as Border).Content as TextBlock).Text;

            for (var i = 0; i < _menuItems.Length; i++)
            {
                var menuItem = _menuItems[i];
                menuItem.IsSelected = menuItem.Name.Equals(name);
                var textColor = menuItem.IsSelected ? Color.Aquamarine : Color.White;
                ((_menuPanel.Children[i + 1] as Border).Content as TextBlock).TextColor = textColor;
                Unload(menuItem);
                if (menuItem.IsSelected) LoadScene(menuItem);
            }
        }

        private void Unload(MenuItems menuItem)
        {
            if (Content.IsLoaded(menuItem.URL))
            {
                Content.Unload(menuItem.URL);
            }
        }

        private void LoadScene(MenuItems menuItem)
        {
            DebugDrawerSystem.Instance.Clear();
            SceneSystem.SceneInstance.RootScene.Children.Clear();
            Scene scene = Content.Load<Scene>(menuItem.URL);
            SceneSystem.SceneInstance.RootScene.Children.Add(scene);
        }
    }

    internal struct MenuItems
    {
        public string Name { get; }
        public string URL { get; }
        public bool IsSelected { get; set; }

        public MenuItems(string name, string url, bool isSelected)
        {
            Name = name;
            URL = url;
            IsSelected = isSelected;
        }
    }
}