using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class Login : ContentPage
    {
        private SQLiteAsyncConnection _con;
        public Login()
        {
            InitializeComponent();
            _con = DependencyService.Get<DataBase>().GetConnection();
        }

        public static IEnumerable<Estudiante> SELECT_WHERE(SQLiteConnection db, string usuario, string contrasena)
        {
            return db.Query<Estudiante>("select * from estudiante where usuario =? and contrasena =?", usuario, contrasena);
        }

        private void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var documentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                db.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = SELECT_WHERE(db, txtUsuario.Text, txtPassword.Text);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new ConsultaRegistro());
                }
                else 
                {
                    DisplayAlert("Alerta", "Usuario no registrado", "ok");
                }
            }
            catch(Exception ex)
            {
                DisplayAlert("Alerta", "Error en el proceso " + ex.Message, "ok");
            }
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }
    }
}