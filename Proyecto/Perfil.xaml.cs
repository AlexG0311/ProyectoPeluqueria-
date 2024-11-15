namespace Proyecto;

public partial class Perfil : ContentPage
{
	public Perfil()
	{
		InitializeComponent();
	}


    private async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        bool confirmDelete = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas eliminar este perfil?", "Sí", "No");
        if (confirmDelete)
        {
            // Lógica de eliminación del perfil
            // Aquí podrías llamar a un método de servicio o eliminar los datos localmente
            await DisplayAlert("Eliminado", "El perfil ha sido eliminado exitosamente.", "OK");
            // Redirigir o actualizar la página si es necesario
        }
    }
}

