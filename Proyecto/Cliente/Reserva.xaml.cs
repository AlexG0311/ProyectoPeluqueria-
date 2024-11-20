using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto
{
    public partial class Reserva : ContentPage
    {
        private readonly ApiService _apiService;
        private int _selectedEmpleadoId;

        public Reserva()
        {
            InitializeComponent();
            _apiService = new ApiService("https://374b-181-78-20-113.ngrok-free.app");
            BindingContext = this; // Establecer el contexto de enlace
            FechaSeleccionada = DateTime.Now;

            if (App.CurrentServicio != null)
            {
                Debug.WriteLine($"Reserva iniciada para el servicio: {App.CurrentServicio.Nombre}");
            }
        }

        // Colección para almacenar los empleados disponibles
        public ObservableCollection<EmpleadoDTO> EmpleadosDisponibles { get; set; } = new ObservableCollection<EmpleadoDTO>();

        // Propiedad para la fecha seleccionada
        public DateTime FechaSeleccionada { get; set; }

        // Propiedad para la hora seleccionada
        public TimeSpan HoraSeleccionada { get; set; }

        // Evento para manejar la selección de un empleado
        private void OnEmpleadoSelected(object sender, SelectionChangedEventArgs e)
        {
            var empleadoSeleccionado = e.CurrentSelection.FirstOrDefault() as EmpleadoDTO;
            if (empleadoSeleccionado != null)
            {
                _selectedEmpleadoId = empleadoSeleccionado.idEmpleado;
                // Mostrar una confirmación visual
                DisplayAlert("Empleado seleccionado", $"Seleccionaste: {empleadoSeleccionado.Nombre}", "OK");
            }
        }



        // Evento para confirmar la reserva
        private async void OnConfirmarReserva(object sender, EventArgs e)
        {
            if (_selectedEmpleadoId == 0)
            {
                await DisplayAlert("Error", "Por favor, seleccione un empleado.", "OK");
                return;
            }

            var horaSeleccionada = HoraSeleccionada;

            // Crear el objeto ReservaDTO
            var reservaDTO = new ReservaDTO
            {
                Fecha = FechaSeleccionada,
                Hora = horaSeleccionada,
                Cliente_idCliente = (App.CurrentUser as ClienteDTOO)?.idCliente ?? 0, // Obtener el ID del cliente
                Servicio_idServicio = App.CurrentServicio.IdServicio, // Obtener el servicio seleccionado
                Empleado_idEmpleado = _selectedEmpleadoId, // ID del empleado seleccionado
                Estado_idEstado = 1 // Estado "Pendiente"
            };

            // Enviar la reserva al backend
            var resultado = await _apiService.PostAsync<ReservaDTO, ReservaDTO>("api/Reservas", reservaDTO);
            if (resultado != null)
            {
                await DisplayAlert("Reserva", "Reserva confirmada exitosamente.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Hubo un problema al confirmar la reserva.", "Cerrar");
            }
        }

        // Evento para manejar el cambio de fecha seleccionada
        private async void OnFechaSeleccionadaChanged(object sender, EventArgs e)
        {
            await ActualizarEmpleadosDisponibles();
        }

        // Evento para manejar el cambio de hora seleccionada
        private async void TimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TimePicker.Time)) // Verificar si cambió la propiedad Time
            {
                HoraSeleccionada = timePicker.Time; // Actualizar la hora seleccionada
                await ActualizarEmpleadosDisponibles(); // Actualizar empleados disponibles
            }
        }

        // Método para actualizar la lista de empleados disponibles
        private async Task ActualizarEmpleadosDisponibles()
        {
            try
            {
                // Consultar al backend para obtener los empleados disponibles
                var empleados = await _apiService.GetAsync<List<EmpleadoDTO>>(
                    $"api/Reservas/EmpleadosDisponibles?fecha={FechaSeleccionada:yyyy-MM-dd}&hora={HoraSeleccionada}&idServicio={App.CurrentServicio.IdServicio}");

                // Limpiar y actualizar la lista de empleados disponibles
                EmpleadosDisponibles.Clear();
                foreach (var empleado in empleados)
                {
                    EmpleadosDisponibles.Add(empleado);
                }

                if (!EmpleadosDisponibles.Any())
                {
                    await DisplayAlert("Información", "No hay empleados disponibles para la fecha y hora seleccionadas.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener empleados disponibles: {ex.Message}");
                await DisplayAlert("Error", "No se pudieron cargar los empleados disponibles.", "Cerrar");
            }
        }
    }
}
