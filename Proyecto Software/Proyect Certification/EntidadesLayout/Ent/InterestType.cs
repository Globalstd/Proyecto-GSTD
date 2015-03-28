using System;using System.Linq;using System.Text;namespace EntidadesLayout.Ent {	[Serializable]	public class InterestType	{		private Guid interestTypeKey;		private string name;		public InterestType(){}		public Guid InterestTypeKey		{			get { return interestTypeKey; }			set { interestTypeKey = value; }		}		public string Name		{			get { return name; }			set { name = value; }		}
    }
}
