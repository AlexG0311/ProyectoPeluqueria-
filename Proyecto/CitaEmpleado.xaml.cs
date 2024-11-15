using Proyecto.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Proyecto;

public partial class CitaEmpleado : ContentPage
{
    public ObservableCollection<Citas> Citas { get; set; }
    public ICommand IrCitasCommand { get; }

    public CitaEmpleado(ObservableCollection<Citas> citas)
    {
        InitializeComponent();
        Citas = citas;
        BindingContext = this;
    }

    private void IrCitas(object sender, TappedEventArgs e)
    {
        var frame = (Frame)sender;
        var itemSelected = frame.BindingContext as Citas;

        if (itemSelected != null)
        {
            NavigationToPage(new DetallesCitasEmpleado(itemSelected));
        }
    }


    private void NavigationToPage(ContentPage page)
    {
        if (App.FlyoutPage != null)
        {
            App.FlyoutPage.Detail.Navigation.PushAsync(page);
            App.FlyoutPage.IsPresented = false;
        }
    }
}