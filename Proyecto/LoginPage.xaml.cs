using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto
{
    public partial class LoginPage : ContentPage
    {
        private ApiService _apiService;

        public LoginPage()
        {
            InitializeComponent();
            _apiService = new ApiService("https://07de-181-78-20-113.ngrok-free.app");
        }

        private async void Login(object sender, EventArgs e)
        {
            try
            {
                // Crear el objeto LoginDto con las credenciales ingresadas
                var loginDto = new LoginDto
                {
                    Correo = CorreoEntry.Text,
                    Contrasena = PasswordEntry.Text
                };

                // Realizar la solicitud POST al endpoint de login
                var tipoUsuario = await _apiService.PostAsync<LoginDto, string>("api/Usuarios/Login", loginDto);
                Console.WriteLine("Respuesta de la API: " + tipoUsuario);
                if (tipoUsuario == "Empleado")
                {
                    await DisplayAlert("Login", "Inicio de sesión exitoso como Empleado", "OK");
                    Application.Current.MainPage = new InicioEmpleado(); // Página principal para empleados
                }
                else if (tipoUsuario == "Cliente")
                {
                    await DisplayAlert("Login", "Inicio de sesión exitoso como Cliente", "OK");
                    Application.Current.MainPage = new Inicio(); // Página principal para clientes
                }
                else
                {
                    // Mostrar error si el tipo de usuario es desconocido
                    await DisplayAlert("Error", "Tipo de usuario desconocido", "Cerrar");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el login: " + ex.Message);
                await DisplayAlert("Error", "Hubo un problema al iniciar sesión. Intenta de nuevo más tarde.", "Cerrar");
            }
        }

        private async void Registrar(object sender, EventArgs e)
        {
            // Redirigir a la página de registro
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
