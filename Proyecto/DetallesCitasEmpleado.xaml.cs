using Proyecto.Model;

namespace Proyecto;

public partial class DetallesCitasEmpleado : ContentPage
{
    public Citas DetalleCita { get; set; }

    public DetallesCitasEmpleado(Citas detalle)
    {
        InitializeComponent();
        DetalleCita = detalle;
        BindingContext = DetalleCita;
    }

    // Método para manejar el botón "Cancelar cita"
    private async void OnCancelarCitaClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmar", "¿Estás seguro de que deseas cancelar esta cita?", "Sí", "No");
        if (confirm)
        {
            // Aquí puedes eliminar la cita de la base de datos o de la colección
            await DisplayAlert("Cancelada", "La cita ha sido cancelada.", "OK");

            // Regresar a la página anterior después de cancelar
            await Navigation.PopAsync();
        }
    }

    // Método para manejar el botón "Completado"
    private async void OnCompletadoClicked(object sender, EventArgs e)
    {
        // Aquí puedes actualizar el estado de la cita en la base de datos
        DetalleCita.Descripcion = "Completado"; // Ejemplo de cómo marcar como completado en la clase
        await DisplayAlert("Completado", "La cita ha sido marcada como completada.", "OK");

    }
}
