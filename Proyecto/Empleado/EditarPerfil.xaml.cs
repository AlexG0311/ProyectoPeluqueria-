using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Proyecto.Model;
using Proyecto.Helpers;

namespace Proyecto
{
    public partial class EditarPerfil : ContentPage
    {
        private ApiService _apiService;
        private int _idEmpleado;

        public EditarPerfil(int idEmpleado)
        {
            if (idEmpleado <= 0)
            {
                throw new ArgumentException("El ID del empleado no es v�lido.");
            }

            InitializeComponent();
            _apiService = new ApiService("https://374b-181-78-20-113.ngrok-free.app");
            _idEmpleado = idEmpleado;
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
                var usuario = await _apiService.GetAsync<UsuarioClienteDTO>("api/Usuarios/" + _idEmpleado);

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
                idUsuario = _idEmpleado,
                nombre = NombreEntry.Text,
                apellidos = ApellidosEntry.Text,
                correo = CorreoEntry.Text,
                contrasena = ContrasenaEntry.Text,
                telefono = TelefonoEntry.Text
            };

            try
            {
                // Enviar la solicitud de actualizaci�n a la API
                var response = await _apiService.PutAsync<UsuarioClienteDTO, UsuarioClienteDTO>(
                    "api/Usuarios/" + usuarioEditDTO.idUsuario, usuarioEditDTO);

                if (response != null)
                {
                    await DisplayAlert("�xito", "Perfil actualizado correctamente.", "OK");
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
}