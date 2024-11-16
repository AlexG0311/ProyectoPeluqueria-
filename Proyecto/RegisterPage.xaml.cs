using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto
{
    public partial class RegisterPage : ContentPage
    {
        private ApiService _apiService;

        public RegisterPage()
        {
            InitializeComponent();
            _apiService = new ApiService("https://ff6e-181-78-20-113.ngrok-free.app"); // Cambia esto a la URL de tu API
        }

        private async void Insertar(object sender, EventArgs e)
        {
            // Crear un objeto de tipo UsuarioClienteDTO para enviar los datos a la API
            UsuarioClienteDTO usuarioCliente = LlenarUsuarioCliente();

            try
            {
                // Llamada al servicio API para registrar el usuario como cliente
                UsuarioClienteDTO result = await _apiService.PostAsync<UsuarioClienteDTO, UsuarioClienteDTO>("api/Usuarios", usuarioCliente);

                if (result != null && result.idUsuario > 0)
                {
                    // Mostrar mensaje de éxito
                    await DisplayAlert("Registro", "¡Usuario registrado exitosamente como cliente!", "Ok");
                }
                else
                {
                    // Mostrar mensaje de error si el registro falla
                    await DisplayAlert("Error", "Hubo un problema al registrar el usuario.", "Cerrar");
                }
            }
            catch (Exception ex)
            {
                // Manejar errores de red o de la API
                await DisplayAlert("Error", $"Hubo un problema al registrar el usuario: {ex.Message}", "Cerrar");
            }
        }

        private UsuarioClienteDTO LlenarUsuarioCliente()
        {
            return new UsuarioClienteDTO
            {
                nombre = NombreEntry.Text,
                apellidos = ApellidosEntry.Text,
                correo = CorreoEntry.Text,
                contrasena = PasswordEntry.Text,
                telefono = TelefonoEntry.Text,
                EsCliente = true // Este campo indica que el usuario es un cliente
            };
        }

        private async void OnLoginTapped(object sender, EventArgs e)
        {
            // Redirigir a la página de inicio de sesión
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
