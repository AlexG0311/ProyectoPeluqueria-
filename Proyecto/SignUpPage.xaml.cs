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

            // Validaci�n b�sica de los campos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                return;
            }

            // L�gica de registro (puedes reemplazarla con la l�gica real de tu proyecto)
            await DisplayAlert("Registro", "Usuario registrado con �xito.", "OK");

            // Redirigir a la p�gina de inicio de sesi�n
            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnLoginTapped(object sender, EventArgs e)
        {
            // Redirigir a la p�gina de inicio de sesi�n
            await Navigation.PushAsync(new LoginPage());
        }
    }
}

