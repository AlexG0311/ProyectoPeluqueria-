using System;
using System.Diagnostics;
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
            _apiService = new ApiService("https://9c76-181-78-20-113.ngrok-free.app");
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
                var loginResponse = await _apiService.PostAsync<LoginDto, LoginResponseDto>("api/Usuarios/Login", loginDto);

                if (loginResponse != null)
                {
                    if (loginResponse.TipoUsuario == "Empleado")
                    {
                        // Guardar el usuario en memoria para la sesión
                        App.CurrentUser = new EmpleadoDTOO
                        {
                            idUsuario = loginResponse.idUsuario,
                            idEmpleado = loginResponse.idEmpleado ?? 0,
                            Nombre = loginResponse.Nombre,
                            Apellidos = loginResponse.Apellidos,
                            Telefono = loginResponse.Telefono
                        };

                        // Verificar si el idEmpleado se asignó correctamente
                        Debug.WriteLine($"Empleado logueado: {loginResponse.Nombre} (ID Empleado: {loginResponse.idEmpleado})");
                        await DisplayAlert("Debug", $"Empleado logueado: {loginResponse.Nombre}, ID: {loginResponse.idEmpleado}", "OK");

                        Application.Current.MainPage = new InicioEmpleado(); // Página principal para empleados
                    }
                    else if (loginResponse.TipoUsuario == "Cliente")
                    {
                        // Guardar el cliente en memoria para la sesión
                        App.CurrentUser = new ClienteDTOO
                        {
                            idUsuario = loginResponse.idUsuario,
                            idCliente = loginResponse.idCliente ?? 0,
                            Nombre = loginResponse.Nombre,
                            Apellidos = loginResponse.Apellidos,
                            Telefono = loginResponse.Telefono
                        };

                        await DisplayAlert("Login", "Inicio de sesión exitoso como Cliente", "OK");
                        Application.Current.MainPage = new Inicio(); // Página principal para clientes
                    }
                    else
                    {
                        await DisplayAlert("Error", "Tipo de usuario desconocido", "Cerrar");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Credenciales incorrectas. Inténtalo de nuevo.", "Cerrar");
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
