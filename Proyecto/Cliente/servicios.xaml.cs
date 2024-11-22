using System.Collections.ObjectModel;
using System.Diagnostics;
using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto;

public partial class Servicios : ContentPage
{
    private readonly ApiService _apiService;

    // Cambiar el tipo a ObservableCollection<Servicio> para servicios
    public ObservableCollection<Servicio> ServiciosMostrados { get; set; } = new ObservableCollection<Servicio>();

    public Servicios()
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
        // Llama al método para cargar servicios
        await CargarServicios();
    }

    private async Task CargarServicios()
    {
        try
        {
            // Llamada a la API para obtener la lista de servicios
            List<Servicio> serviciosDesdeApi = await _apiService.GetAsync<List<Servicio>>("api/Servicios");

            // Limpiar la colección actual
            ServiciosMostrados.Clear();

            // Agregar solo los primeros 3 servicios
            foreach (var servicio in serviciosDesdeApi)
            {
                ServiciosMostrados.Add(servicio);
            }
        }
        catch (Exception ex)
        {
            // Manejo de errores
            await DisplayAlert("Error", $"No se pudo cargar los servicios: {ex.Message}", "OK");
        }
    }
}
