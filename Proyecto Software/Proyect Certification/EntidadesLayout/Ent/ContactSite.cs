using System;
using System.Linq;
using System.Text;
namespace EntidadesLayout.Ent
{
    [Serializable]
    public class ContactSite
    {
        private Guid contactSiteKey;
        private string name;
        private string phone;
        private string email;
        private string position;
        private DateTime? startDate;
        private Guid? siteFk;
        private Guid? contactTypeKey;
        private string apPaterno;
        private string apMaterno;
        private Guid? clienteFk;
        public ContactSite() { }

        public Guid ContactSiteKey
        {
            get { return contactSiteKey; }
            set { contactSiteKey = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Position
        {
            get { return position; }
            set { position = value; }
        }
        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public Guid? SiteFk
        {
            get { return siteFk; }
            set { siteFk = value; }
        }
        public Guid? ContactTypeKey
        {
            get { return contactTypeKey; }
            set { contactTypeKey = value; }
        }
        public string ApPaterno
        {
            get { return apPaterno; }
            set { apPaterno = value; }
        }
        public string ApMaterno
        {
            get { return apMaterno; }
            set { apMaterno = value; }
        }
        public Guid? ClienteFk
        {
            get { return clienteFk; }
            set { clienteFk = value; }
        }
    }
}
