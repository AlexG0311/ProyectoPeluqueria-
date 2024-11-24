
using Proyecto.Helpers;
using Proyecto.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Proyecto;

public partial class DetalleEmpleado : ContentPage
{

    private readonly ApiService _apiService;

    // ObservableCollection para enlazar las citas con la vista
    public ObservableCollection<CitaDTO> Citas { get; set; } = new ObservableCollection<CitaDTO>();
   
    public ObservableCollection<Servicio> Servicio { get; set; }  // Cambié el nombre a "Servicios" para mayor claridad

    public DetalleEmpleado()
    {
        InitializeComponent();
        _apiService = new ApiService(App.ApiBaseUrl); // Cambia la URL según tu configuración
        BindingContext = this;
        CargarCitas();
    }

    private async void CargarCitas()
    {
        try
        {
            // ID del empleado (esto debería venir del usuario actual autenticado)
            int idEmpleado = (App.CurrentUser as EmpleadoDTOO)?.idEmpleado ?? 0;

            // Llama al endpoint para obtener las reservas del empleado
            var citasDesdeApi = await _apiService.GetAsync<List<CitaDTO>>($"api/Reservas/Empleado/{idEmpleado}/Reservas");

            // Limpiar y agregar las citas al ObservableCollection
            Citas.Clear();
            foreach (var cita in citasDesdeApi)
            {
                Debug.WriteLine($"Cita cargada: {cita.NombreCompleto}, Fecha: {cita.FechaHora}, Estado: {cita.EstadoDescripcion}");
                Citas.Add(cita);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar las citas: {ex.Message}", "Cerrar");
        }
    }



    private async void VerMaServicios(object sender, EventArgs e)  // Cambié a "VerMasServicios" para mayor claridad
    {
        // Navegar a la página de servicios
        await Navigation.PushAsync(new ServicioEmpleado(Servicio));
    }

    private async void VerMasCitas(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CitaEmpleado());
    }

    private void IrServicio(object sender, TappedEventArgs e)
    {
        var frame = (Frame)sender;
        var itemSelected = frame.BindingContext as Servicio;

        if (itemSelected != null)
        {
            // Navegar a la página "DetalleServicios" pasando el objeto seleccionado
            NavigationToPage(new DetalleServicioEmpleado(itemSelected));
        }
    }

    private void IrCitas(object sender, TappedEventArgs e)
    {
        var frame = (Frame)sender;
        var itemSelected = frame.BindingContext as Citas;

        if (itemSelected != null)
        {
            NavigationToPage(new DetallesCitasEmpleado(itemSelected));
        }
    }


    private void NavigationToPage(ContentPage page)
    {
        if (App.InicioEmpleado != null)
        {
            App.InicioEmpleado.Detail.Navigation.PushAsync(page);
            App.InicioEmpleado.IsPresented = false;
        }
    }


}