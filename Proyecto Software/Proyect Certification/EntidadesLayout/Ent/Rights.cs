﻿using System;using System.Linq;using System.Text;namespace EntidadesLayout.Ent {	[Serializable]	public class Rights	{		private Guid rightKey;		private string name;		private string description;		private int? orden;		public Rights(){}		public Guid RightKey		{			get { return rightKey; }			set { rightKey = value; }		}		public string Name		{			get { return name; }			set { name = value; }		}		public string Description		{			get { return description; }			set { description = value; }		}		public int? Orden		{			get { return orden; }			set { orden = value; }		}	}}

