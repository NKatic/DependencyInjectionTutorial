using System.Windows;
using PeopleViewer.Common;
using PeopleViewer.Presentation;
using PersonDataReader.CSV;
using PersonDataReader.Decorators;
using PersonDataReader.Service;
using PersonDataReader.SQL;

namespace PeopleViewer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ComposeObjects();
            Application.Current.MainWindow.Title = "With Dependency Injection";
            Application.Current.MainWindow.Show();
        }

        private static void ComposeObjects()
        {
            IPersonReader wrappedReader = new CSVReader();
            IPersonReader reader = new CachingReader(wrappedReader);
            PeopleViewModel viewModel = new PeopleViewModel(reader);
            Application.Current.MainWindow = new PeopleViewerWindow(viewModel);
        }
    }
}
