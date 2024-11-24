using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto
{
    public partial class CitaEmpleado : ContentPage
    {
        private readonly ApiService _apiService;

        // ObservableCollection para enlazar las citas con la vista
        public ObservableCollection<CitaDTO> Citas { get; set; } = new ObservableCollection<CitaDTO>();

        public CitaEmpleado()
        {
            InitializeComponent();
            _apiService = new ApiService(App.ApiBaseUrl); // Cambia la URL según tu configuración
            BindingContext = this;

            // Cargar las citas al inicializar la página
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


    }
}
