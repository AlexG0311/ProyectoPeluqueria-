using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto
{
    public partial class Reserva : ContentPage
    {
        private ApiService _apiService;

        public Reserva()
        {
            InitializeComponent();
            _apiService = new ApiService("https://07de-181-78-20-113.ngrok-free.app"); // Cambia esto a la URL de tu API
        }

        private async void InsertarReserva(object sender, EventArgs e)
        {
            // Crear un objeto de tipo ReservaDTO para enviar los datos a la API
            ReservaDTO reserva = LlenarReserva();

            try
            {
                // Llamada al servicio API para registrar la reserva
                ReservaDTO result = await _apiService.PostAsync<ReservaDTO, ReservaDTO>("api/Reservas", reserva);

                if (result != null && result.idReserva > 0)
                {
                    // Mostrar mensaje de éxito
                    await DisplayAlert("Reserva", "¡Reserva registrada exitosamente!", "Ok");
                }
                else
                {
                    // Mostrar mensaje de error si el registro falla
                    await DisplayAlert("Error", "Hubo un problema al registrar la reserva.", "Cerrar");
                }
            }
            catch (Exception ex)
            {
                // Manejar errores de red o de la API
                await DisplayAlert("Error", $"Hubo un problema al registrar la reserva: {ex.Message}", "Cerrar");
            }
        }

        private ReservaDTO LlenarReserva()
        {
            // Asegurarse de que haya una fecha seleccionada
            DateTime fechaSeleccionada = calendar.SelectedDate ?? DateTime.Now;

            return new ReservaDTO
            {
            //    Fecha = fechaSeleccionada,
              //  Hora = HoraPicker.Time,
                //Cliente_idCliente = int.Parse(ClienteEntry.Text), // Asegúrate de tener el ID del cliente
       //         Servicio_idServicio = int.Parse(ServicioEntry.Text), // Asegúrate de tener el ID del servicio
         //       Asignacion_idAsignacion = int.Parse(AsignacionEntry.Text), // ID de la asignación, si aplica
           //     Estado_idEstado = int.Parse(EstadoEntry.Text) // Estado de la reserva, si aplica
            };
        }

        private async void OnCancelarTapped(object sender, EventArgs e)
        {
            // Regresar a la página anterior
            await Navigation.PopAsync();
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            // Este método se ejecuta cuando se selecciona una fecha en el calendario
            DateTime fechaSeleccionada = e.NewDate;
            Console.WriteLine("Fecha seleccionada: " + fechaSeleccionada.ToString("yyyy-MM-dd"));
        }
    }
}
