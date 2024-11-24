using Proyecto.Model;
using System.Collections.ObjectModel;

namespace Proyecto
{
    public partial class App : Application
    {
        public static Producto selectedServicio;

        public static string ApiBaseUrl { get; } = "https://cde9-181-78-20-113.ngrok-free.app";

        public static ObservableCollection<Producto> ProductosCombos;

        public static ObservableCollection<Producto> CarritoProductos { get; set; } = new ObservableCollection<Producto>();

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
