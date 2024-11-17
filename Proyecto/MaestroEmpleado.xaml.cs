using Proyecto.Model;
using System.Collections.ObjectModel;

namespace Proyecto;

public partial class MaestroEmpleado : ContentPage
{
    public ObservableCollection<Citas> Citas { get; set; }
    public ObservableCollection<Servicio> Servicio { get; set; }
    public MaestroEmpleado()
    {
        InitializeComponent();
    }
    private void InicioEmpleado(object sender, EventArgs e)
    {
        NavigationToPage(new DetalleEmpleado());
    }


    private void Servicios(object sender, EventArgs e)
    {
        NavigationToPage(new ServicioEmpleado(Servicio));
    }

    private void Cita(object sender, EventArgs e)
    {
        NavigationToPage(new CitaEmpleado());
    }

    private void Historial(object sender, EventArgs e)
   {
     NavigationToPage(new Historial());
    }

   

    private void Perfil(object sender, EventArgs e)
    {
        NavigationToPage(new Perfil());
    }





    private async void VerMaServicios(object sender, EventArgs e)
    {
        // Navegar a la página de destino (reemplaza DestinationPage con la página correspondiente)
        await Navigation.PushAsync(new Servicios());
    }

    private void NavigationToPage(ContentPage page)
    {
        App.InicioEmpleado.Detail.Navigation.PushAsync(page);
        App.InicioEmpleado.IsPresented = false;
    }
}