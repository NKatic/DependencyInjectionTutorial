using Autofac;
using Autofac.Configuration;
using Autofac.Features.ResolveAnything;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace PeopleViewer.Autofac.LateBinding
{
    public partial class App : Application
    {
        IContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Title = "With Dependency Injection - Autofac Late Binding";
            Application.Current.MainWindow.Show();
        }

        // Ne postoje reference na PersonDataReader projekte. Ako su nam potrebni, dodat ćemo ih u runtime-u.
        // Assemblije koji su nam potrebni stavimo u folder s exe-om.
        // Za ovaj demo imamo Post-build evente koji kopira sve iz LateBindingAssemblies foldera u targetDir tj output folder.
        // Trebamo nekoliko nugeta: Autofac, Autofac.Configuration, Microsoft.Extensions.Configuration i Microsoft.Extensions.Configuration.Json (sve ima u Autofac dokumentaciji za late binding)
        private void ConfigureContainer()
        {
            // Kreiraj novi builder i dodaj json konfiguracijski file.
            ConfigurationBuilder config = new ConfigurationBuilder();
            config.AddJsonFile("autofac.json");

            ConfigurationModule module = new ConfigurationModule(config.Build());
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(module);

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource()); // Ovo je za potrebe demo-a, u produkciji treba registrirati sve tipove eksplicitno, jer je ovo sporo (koristi reflection).

            Container = builder.Build();
        }

        private void ComposeObjects()
        {
            Application.Current.MainWindow = Container.Resolve<PeopleViewerWindow>();
        }
    }
}
