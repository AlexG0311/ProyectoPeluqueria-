using Proyecto.Cliente;

namespace Proyecto;

public partial class Maestro : ContentPage
{
	public Maestro()
	{
		InitializeComponent();
	}

    private void IrInicio(object sender, EventArgs e)
    {
        NavigationToPage(new Detalle());
    }

    private void IrCarrito(object sender, EventArgs e)
    {
        NavigationToPage(new CarritoPage());

    }


    private void IrPerfil(object sender, EventArgs e)
    {
        NavigationToPage(new PerfilCliente());
    }

    private void NavigationToPage(ContentPage page)
    {
        App.FlyoutPage.Detail.Navigation.PushAsync(page);
        App.FlyoutPage.IsPresented = false;
    }
}