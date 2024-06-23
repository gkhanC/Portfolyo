/*
______________________________________________________
 * Created By: Gökhan Tutku Çay
 * Email: caygkhan@gmail.com
 * 
 * Date: May 11 2023
 * 
 * Copyright (c) 2023 Gökhan Tutku Çay. All rights reserved.
______________________________________________________
*/

using System;
using HypeFire.Templates.Runner.Managers.GameSettings.Constraints;

namespace HypeFire.Templates.Runner.Managers.GameSettings
{
	[Serializable]
	public class GameConstraints
	{
		public bool usePositionalConstraint = false;
		public PositionalConstraint moveAbleConstraints = new PositionalConstraint();
	}
}