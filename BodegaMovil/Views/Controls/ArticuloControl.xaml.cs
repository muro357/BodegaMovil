using System.Runtime.CompilerServices;

namespace BodegaMovil.Views.Controls;

public partial class ArticuloControl : ContentView
{
    public bool IsForSurtir { get; set; }
    public bool IsForAdd { get; set; }
    public ArticuloControl()
	{
		InitializeComponent();
	}

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (IsForAdd && !IsForSurtir)
        {
            btnSave.SetBinding(Button.CommandProperty, "AddNewArtCommand");
        }
        else if (!IsForAdd && IsForSurtir)
        {
            btnSave.SetBinding(Button.CommandProperty, "SurtirArtCommand");
        }
    }
}