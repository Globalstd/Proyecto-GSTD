﻿using System;using System.Linq;using System.Text;namespace EntidadesLayout.Ent {	[Serializable]	public class MD5QMS	{		private Guid mD5QMSKey;		private double? stageI;		private double? stageII;		private double? recertification;		private double? seg;		private double? annual;		private int? high;		private int? low;		private Guid? standardFk;		public MD5QMS(){}		public Guid MD5QMSKey		{			get { return mD5QMSKey; }			set { mD5QMSKey = value; }		}		public double? StageI		{			get { return stageI; }			set { stageI = value; }		}		public double? StageII		{			get { return stageII; }			set { stageII = value; }		}		public double? Recertification		{			get { return recertification; }			set { recertification = value; }		}		public double? Seg		{			get { return seg; }			set { seg = value; }		}		public double? Annual		{			get { return annual; }			set { annual = value; }		}		public int? High		{			get { return high; }			set { high = value; }		}		public int? Low		{			get { return low; }			set { low = value; }		}		public Guid? StandardFk		{			get { return standardFk; }			set { standardFk = value; }		}	}}
