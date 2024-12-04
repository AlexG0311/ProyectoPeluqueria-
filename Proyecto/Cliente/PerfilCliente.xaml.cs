using Proyecto.Model;
using System.Text.Json;

namespace Proyecto.Cliente;

public partial class PerfilCliente : ContentPage
{
    public ClienteDTOO Cliente { get; set; }
    private int _idCliente; // Asegúrate de declarar esta variable
    public PerfilCliente()
	{
		InitializeComponent();

        BindingContext = this;
        CargarCliente(); // Carga el empleado automáticamente
    }


    private async void CargarCliente()
    {
        try
        {
            // Obtener el ID del usuario actual desde App.CurrentUser obtener()
            int idUsuario = (App.CurrentUser as ClienteDTOO)?.idUsuario ?? 0;

            if (idUsuario == 0)
            {
                await DisplayAlert("Error", "No se encontró información del empleado autenticado.", "Cerrar");
                return;
            }

            // URL base de la API
            string baseUrl = "https://154b-181-78-20-113.ngrok-free.app";

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
                    Cliente = JsonSerializer.Deserialize<ClienteDTOO>(json);

                    if (Cliente != null)
                    {
                        BindingContext = Cliente; // Actualizar el contexto de enlace
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
            int idUsuario = (App.CurrentUser as ClienteDTOO)?.idUsuario ?? 0;

            if (idUsuario > 0)
            {
                // Navegar a la página de edición de perfil pasando el ID del usuario
                await Navigation.PushAsync(new EditarPerfil(idUsuario));
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
}