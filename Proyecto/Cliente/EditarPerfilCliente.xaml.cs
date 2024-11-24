using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto.Cliente;

public partial class EditarPerfilCliente : ContentPage
{
    private ApiService _apiService;
    private int _idCliente;
    public EditarPerfilCliente(int idCliente)
    {
        if (idCliente <= 0)
        {
            throw new ArgumentException("El ID del empleado no es válido.");
        }



        InitializeComponent();


        _apiService = new ApiService(App.ApiBaseUrl);
        _idCliente = idCliente;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarDatosEmpleado();
    }

    private async Task CargarDatosEmpleado()
    {
        try
        {
            // Solicitar los datos desde la API usando el ID del empleado
            var usuario = await _apiService.GetAsync<UsuarioClienteDTO>("api/Usuarios/" + _idCliente);

            if (usuario != null)
            {
                NombreEntry.Text = usuario.nombre;
                ApellidosEntry.Text = usuario.apellidos;
                CorreoEntry.Text = usuario.correo;
                TelefonoEntry.Text = usuario.telefono;
                ContrasenaEntry.Text = usuario.contrasena;
            }
            else
            {
                await DisplayAlert("Error", "No se encontraron datos del perfil.", "Cerrar");
            }
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlert("Error", $"Error de red: {ex.Message}", "Cerrar");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al cargar el perfil: {ex.Message}", "Cerrar");
        }
    }

    private async void ActualizarPerfilEmpleado(object sender, EventArgs e)
    {
        var usuarioEditDTO = new UsuarioClienteDTO
        {
            idUsuario = _idCliente,
            nombre = NombreEntry.Text,
            apellidos = ApellidosEntry.Text,
            correo = CorreoEntry.Text,
            contrasena = ContrasenaEntry.Text,
            telefono = TelefonoEntry.Text
        };

        try
        {
            // Enviar la solicitud de actualización a la API
            var response = await _apiService.PutAsync<UsuarioClienteDTO, UsuarioClienteDTO>(
                "api/Usuarios/" + usuarioEditDTO.idUsuario, usuarioEditDTO);

            if (response != null)
            {
                await DisplayAlert("Éxito", "Perfil actualizado correctamente.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar el perfil.", "Cerrar");
            }
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlert("Error", $"Error de red: {ex.Message}", "Cerrar");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al actualizar el perfil: {ex.Message}", "Cerrar");
        }
    }

}