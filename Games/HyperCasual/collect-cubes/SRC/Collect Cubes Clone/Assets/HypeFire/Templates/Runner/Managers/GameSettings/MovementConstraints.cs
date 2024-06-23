using System;
using HypeFire.Utilities.CustomStructures;

namespace HypeFire.Templates.Runner.Managers.GameSettings
{
	[Serializable]
	public class MovementConstraints
	{
		public MinMaxFloat xConstraint = new MinMaxFloat(-5f, 5f);
		public MinMaxFloat xConstraintOffset = new MinMaxFloat(-5f, 5f);

		public MinMaxFloat zConstraint = new MinMaxFloat(-5f, 5f);
		public MinMaxFloat zConstraintOfsset = new MinMaxFloat(-5f, 5f);
	}
}