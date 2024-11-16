using Proyecto.Model;
using System.Collections.ObjectModel;

namespace Proyecto
{
    public partial class App : Application
    {
        public static Producto selectedServicio;

        public static ObservableCollection<Producto> ProductosCombos;


        public static FlyoutPage FlyoutPage { get; set; }

        public static FlyoutPage InicioEmpleado { get; internal set; }
        public static IUsuarioDTO CurrentUser { get; set; }


        public static Servicio CurrentServicio { get; set; } 
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
