using System;
using System.Linq;
using System.Text;
namespace EntidadesLayout.Ent
{
    [Serializable]
    public class Cliente
    {
        private Guid clientekey;
        private string claveCliente;
        private string nombreCliente;
        private string razonSocial;
        private string rFC;
        private string street;
        private string suburb;
        private string delegation;
        private string town;
        private string zip;
        private string numExt;
        private string numInt;
        private string webSite;
        private string perfil;
        private string archivoPerfil;
        private bool? isCliente;
        private Guid? typeCertificationFk;
        private Guid? stateFk;
        private Guid? officeFk;
        private DateTime? dateProspect;
        private DateTime? dateCliente;
        private Guid? sourceClientFk;
        private string recoment;
        private Guid? statusClienteFk;
        private string nickName;
        private string pass;
        private bool? baja;
        public Cliente() { }

        public Guid Clientekey
        {
            get { return clientekey; }
            set { clientekey = value; }
        }
        public string ClaveCliente
        {
            get { return claveCliente; }
            set { claveCliente = value; }
        }
        public string NombreCliente
        {
            get { return nombreCliente; }
            set { nombreCliente = value; }
        }
        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }
        public string RFC
        {
            get { return rFC; }
            set { rFC = value; }
        }
        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        public string Suburb
        {
            get { return suburb; }
            set { suburb = value; }
        }
        public string Delegation
        {
            get { return delegation; }
            set { delegation = value; }
        }
        public string Town
        {
            get { return town; }
            set { town = value; }
        }
        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }
        public string NumExt
        {
            get { return numExt; }
            set { numExt = value; }
        }
        public string NumInt
        {
            get { return numInt; }
            set { numInt = value; }
        }
        public string WebSite
        {
            get { return webSite; }
            set { webSite = value; }
        }
        public string Perfil
        {
            get { return perfil; }
            set { perfil = value; }
        }
        public string ArchivoPerfil
        {
            get { return archivoPerfil; }
            set { archivoPerfil = value; }
        }
        public bool? IsCliente
        {
            get { return isCliente; }
            set { isCliente = value; }
        }
        public Guid? TypeCertificationFk
        {
            get { return typeCertificationFk; }
            set { typeCertificationFk = value; }
        }
        public Guid? StateFk
        {
            get { return stateFk; }
            set { stateFk = value; }
        }
        public Guid? OfficeFk
        {
            get { return officeFk; }
            set { officeFk = value; }
        }
        public DateTime? DateProspect
        {
            get { return dateProspect; }
            set { dateProspect = value; }
        }
        public DateTime? DateCliente
        {
            get { return dateCliente; }
            set { dateCliente = value; }
        }
        public Guid? SourceClientFk
        {
            get { return sourceClientFk; }
            set { sourceClientFk = value; }
        }
        public string Recoment
        {
            get { return recoment; }
            set { recoment = value; }
        }
        public Guid? StatusClienteFk
        {
            get { return statusClienteFk; }
            set { statusClienteFk = value; }
        }
        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }
        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }
        public bool? Baja
        {
            get { return baja; }
            set { baja = value; }
        }


    }
}
