using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Proyecto.Model;
using Proyecto.Cliente;

namespace Proyecto.Empleado
{
    public partial class PerfilEmpleado : ContentPage
    {
        public EmpleadoDTOO Empleado { get; set; }
        private int _idEmpleado; // Asegúrate de declarar esta variable


        public PerfilEmpleado() // Constructor sin parámetros
        {
            InitializeComponent();
            BindingContext = this;
            CargarEmpleado(); // Carga el empleado automáticamente
        }

        private async void CargarEmpleado()
        {
            try
            {
                // Obtener el ID del usuario actual desde App.CurrentUser
                int idUsuario = (App.CurrentUser as EmpleadoDTOO)?.idUsuario ?? 0;

                if (idUsuario == 0)
                {
                    await DisplayAlert("Error", "No se encontró información del empleado autenticado.", "Cerrar");
                    return;
                }

                // URL base de la API
                string baseUrl = "https://990b-190-0-245-162.ngrok-free.app";

                // Endpoint de la API para obtener el empleado
                string endpoint = $"/api/Usuarios/{idUsuario}";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    // Hacer la solicitud GET
                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como un objeto de tipo EmpleadoDTOO
                        string json = await response.Content.ReadAsStringAsync();
                        Empleado = JsonSerializer.Deserialize<EmpleadoDTOO>(json);

                        if (Empleado != null)
                        {
                            BindingContext = Empleado; // Actualizar el contexto de enlace
                        }
                        else
                        {
                            await DisplayAlert("Error", "No se pudo cargar la información del empleado.", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", $"Error al obtener los datos: {response.StatusCode}", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo cargar la información del empleado: {ex.Message}", "OK");
            }
        }





        // Método para cerrar sesión
        private async void OnLogOutClicked(object sender, EventArgs e)
        {
            App.CurrentUser = null; // Limpiar el usuario actual
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        // Botón para editar el perfil
        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del usuario desde App.CurrentUser o el objeto actual
                int idUsuario = (App.CurrentUser as EmpleadoDTOO)?.idUsuario ?? 0;

                if (idUsuario > 0)
                {
                    // Navegar a la página de edición de perfil pasando el ID del usuario
                    await Navigation.PushAsync(new EditarPerfilCliente(idUsuario));
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo cargar el perfil del usuario.", "Cerrar");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al navegar a EditarPerfil: {ex.Message}");
                await DisplayAlert("Error", "No se pudo abrir la página de edición de perfil.", "Cerrar");
            }
        }


        // Lógica del botón "Cambiar Avatar"
        private async void OnChangeAvatarClicked(object sender, EventArgs e)
        {
            var avatarSelection = await DisplayActionSheet("Selecciona un Avatar", "Cancelar", null, "Avatar 1", "Avatar 2", "Avatar 3", "Avatar 4", "Avatar 5");

            switch (avatarSelection)
            {
                case "Avatar 1":
                    AvatarImage.Source = "banner.jpg";
                    break;
                case "Avatar 2":
                    AvatarImage.Source = "carrito.png";
                    break;
                case "Avatar 3":
                    AvatarImage.Source = "empleado.png";
                    break;
                case "Avatar 4":
                    AvatarImage.Source = "banner.jpg";
                    break;
                case "Avatar 5":
                    AvatarImage.Source = "banner.jpg";
                    break;
                default:
                    break;
            }
        }

        // Método de navegación
        private async Task NavigationToPage(ContentPage page)
        {
            if (App.FlyoutPage != null && App.FlyoutPage.Detail != null && App.FlyoutPage.Detail.Navigation != null)
            {
                await App.FlyoutPage.Detail.Navigation.PushAsync(page);
                App.FlyoutPage.IsPresented = false;
            }
            else
            {
                // Mostrar un error si no se pudo acceder a la navegación
                await DisplayAlert("Error", "No se pudo acceder a la navegación.", "Cerrar");
            }
        }
    }
}
