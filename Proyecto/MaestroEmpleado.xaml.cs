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
        NavigationToPage(new Detalle());
    }


    private void Servicios(object sender, EventArgs e)
    {
        NavigationToPage(new ServicioEmpleado(Servicio));
    }

    private void Cita(object sender, EventArgs e)
    {
        NavigationToPage(new CitaEmpleado(Citas));
    }

    private void Historial(object sender, EventArgs e)
    {
        NavigationToPage(new Historial());
    }

    private void Perfil(object sender, EventArgs e)
    {
        NavigationToPage(new Perfil());
    }

    private void NavigationToPage(ContentPage page)
    {
        App.FlyoutPage.Detail.Navigation.PushAsync(page);
        App.FlyoutPage.IsPresented = false;
    }
}