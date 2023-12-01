
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Threading.Tasks;

namespace CMP.Servicios
{
    public class ConexionFirebase
    {        
        public static FirebaseClient FBCliente = new FirebaseClient("https://cmpsoft-260301-default-rtdb.firebaseio.com/");

        public static FirebaseStorage FBClienteStorage = new FirebaseStorage("cmpsoft-260301.appspot.com");
    }
}
