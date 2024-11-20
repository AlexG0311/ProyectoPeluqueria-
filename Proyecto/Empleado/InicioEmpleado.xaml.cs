namespace Proyecto;

public partial class InicioEmpleado : FlyoutPage
{
	public InicioEmpleado()
	{
		InitializeComponent();

        Flyout = new MaestroEmpleado();
        Detail = new NavigationPage(new DetalleEmpleado());

        App.InicioEmpleado = this;


    }
}