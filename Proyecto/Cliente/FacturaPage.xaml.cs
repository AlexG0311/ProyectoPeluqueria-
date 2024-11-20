using System;
using System.Collections.Generic;
using Proyecto.Model;


namespace Proyecto.Cliente;

public partial class FacturaPage : ContentPage
{
    public List<Producto> ProductosComprados { get; set; }
    public double TotalCompra { get; set; }
    public string FechaCompra { get; set; }

    public FacturaPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Obtener los productos comprados del carrito (puedes acceder a App.CarritoProductos)
        ProductosComprados = App.CarritoProductos.ToList();

        // Calcular el total de la compra
        TotalCompra = 0;
        foreach (var producto in ProductosComprados)
        {
            TotalCompra += (double)producto.Precio * (double)producto.Cantidad;

        }

        // Obtener la fecha actual
        FechaCompra = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

        // Limpiar el carrito después de la compra
        App.CarritoProductos.Clear();
    }
}