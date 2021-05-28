using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Elemento : ContentPage
    {
        private SQLiteAsyncConnection _con;
        private int IdSeleccionado;
        IEnumerable<Estudiante> ResultadoDelete;
        IEnumerable<Estudiante> ResultadoUpdate;
        private ObservableCollection<Estudiante> _tablaEstudiante;

        public Elemento(int Id,string Nombre, string Usuario, string Contrasena)
        {
            InitializeComponent();
            IdSeleccionado = Id;
            txtCodigo.Text = Id.ToString();
            txtNombre.Text = Nombre;
            txtUsuario.Text = Usuario;
            txtPassword.Text = Contrasena;
            _con = DependencyService.Get<DataBase>().GetConnection();
        }
        
        public static IEnumerable<Estudiante> Delete(SQLiteConnection db, int Id)
        {
            return db.Query<Estudiante>("DELETE FROM Estudiante where Id = ?", Id);
        }

        public static IEnumerable<Estudiante> Update(SQLiteConnection db, string nombre, string usuario, string contrasena, int Id)
        {
            return db.Query<Estudiante>("UPDATE Estudiante SET Nombre = ?,  Usuario =?, Contrasena =? where Id = ?", nombre, usuario, contrasena, Id);
        }

        private void btnUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                ResultadoDelete = Update(db, txtNombre.Text, txtUsuario.Text, txtPassword.Text, IdSeleccionado);
                DisplayAlert("Alerta", "Se actualizó correctamente el registro", "ok");
            }
            catch(Exception ex)
            {
                DisplayAlert("Alerta", "Error"+ex.Message, "ok");
            }
        }

        private void btnDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                ResultadoDelete = Delete(db, IdSeleccionado);
                DisplayAlert("Alerta", "Se eliminó correctamente el registro", "ok");
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "Error" + ex.Message, "ok");
            }
        }

        private async void btnRegresa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConsultaRegistro());
        }
    }
}