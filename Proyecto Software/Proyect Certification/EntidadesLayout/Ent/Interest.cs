using System;
using System.Linq;
using System.Text;
namespace EntidadesLayout.Ent
{
    [Serializable]
    public class Interest
    {
        private Guid interestKey;
        private Guid? standardFk;
        private Guid? interestTypeFk;
        private Guid? standardCourseFk;
        private Guid? clienteFk;
        public Interest() { }

        public Guid InterestKey
        {
            get { return interestKey; }
            set { interestKey = value; }
        }
        public Guid? StandardFk
        {
            get { return standardFk; }
            set { standardFk = value; }
        }
        public Guid? InterestTypeFk
        {
            get { return interestTypeFk; }
            set { interestTypeFk = value; }
        }
        public Guid? StandardCourseFk
        {
            get { return standardCourseFk; }
            set { standardCourseFk = value; }
        }
        public Guid? ClienteFk
        {
            get { return clienteFk; }
            set { clienteFk = value; }
        }
    }
}
