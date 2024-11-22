using System.Collections.ObjectModel;
using System.Diagnostics;
using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto;

public partial class Productos : ContentPage
{
    private readonly ApiService _apiService;

    // Colección para almacenar los productos mostrados
    public ObservableCollection<Producto> ProductosMostrados { get; set; } = new ObservableCollection<Producto>();

    public Productos()
    {
        InitializeComponent();

        // Inicializar ApiService con la URL correcta
        _apiService = new ApiService("https://9c76-181-78-20-113.ngrok-free.app");
        BindingContext = this;

        // Cargar datos al iniciar la página
        CargarDatos();
    }

    private async void CargarDatos()
    {
        // Llama al método para cargar productos
        await CargarProductos();
    }

    private async Task CargarProductos()
    {
        try
        {
            // Llamada a la API para obtener la lista de productos
            List<Producto> productosDesdeApi = await _apiService.GetAsync<List<Producto>>("api/Productoes");

            // Limpiar la colección actual
            ProductosMostrados.Clear();

            // Agregar solo los primeros 3 productos
            foreach (var producto in productosDesdeApi)
            {
                ProductosMostrados.Add(producto);
            }
        }
        catch (Exception ex)
        {
            // Manejo de errores
            await DisplayAlert("Error", $"No se pudo cargar los productos: {ex.Message}", "OK");
        }
    }
}
