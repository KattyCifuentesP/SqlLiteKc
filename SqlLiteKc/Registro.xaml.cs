using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SqlLiteKc.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqlLiteKc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        private SQLiteAsyncConnection _con;
        public Registro()
        {
            InitializeComponent();
            _con = DependencyService.Get<DataBase>().GetConnection();
        }

        private void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var Datos = new Estudiante { Nombre = txtNombre.Text, Usuario = txtUsuario.Text, Contrasena = txtPassword.Text };
                _con.InsertAsync(Datos);
                DisplayAlert("Alerta", "Datos registrados", "ok");
                txtNombre.Text = "";
                txtUsuario.Text = "";
                txtPassword.Text = "";
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "Error en el proceso "+ ex.Message, "ok");
            }
        }

        private async void btnRegresa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}