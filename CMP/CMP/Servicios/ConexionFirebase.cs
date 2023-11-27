
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Threading.Tasks;

namespace CMP.Servicios
{
    public class ConexionFirebase
    {        
        public static FirebaseClient FBCliente = new FirebaseClient("https://cmpsoft-260301-default-rtdb.firebaseio.com/");
    }


    public class ConexionFBStorage
    {
        public static FirebaseStorage FBClienteStorage = new FirebaseStorage("gs://cmpsoft-260301.appspot.com/");
    }
}
