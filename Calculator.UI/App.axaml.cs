using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Calculator.Core;
using Calculator.UI.ViewModels;
using Calculator.UI.Views;

namespace Calculator.UI
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                ExpressionParser parser = new ExpressionParser();
                Interpreter interpreter = new Interpreter();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(interpreter, parser),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}