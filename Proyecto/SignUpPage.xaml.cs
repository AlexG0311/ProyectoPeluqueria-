using System;
using Microsoft.Maui.Controls;

namespace Proyecto
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            string nombre = NombreEntry.Text;
            string apellidos = ApellidosEntry.Text;
            string correo = CorreoEntry.Text;
            string password = PasswordEntry.Text;

            // Validación básica de los campos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                return;
            }

            // Lógica de registro (puedes reemplazarla con la lógica real de tu proyecto)
            await DisplayAlert("Registro", "Usuario registrado con éxito.", "OK");

            // Redirigir a la página de inicio de sesión
            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnLoginTapped(object sender, EventArgs e)
        {
            // Redirigir a la página de inicio de sesión
            await Navigation.PushAsync(new LoginPage());
        }
    }
}

