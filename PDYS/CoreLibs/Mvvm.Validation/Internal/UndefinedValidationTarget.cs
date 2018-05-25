using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mvvm.Validation.Internal
{
	internal class UndefinedValidationTarget : IValidationTarget
	{
		private static readonly object FakeTarget = new object();

		#region IValidationTarget Members

		public IEnumerable<object> UnwrapTargets()
		{
			return new[] {FakeTarget};
		}

        public int Undefined { get; set; }
        public IEnumerable<Expression<Func<object>>> UnwrapExpressions()
        {
            return new Expression<Func<object>>[]{ ()=> Undefined };
        }

		public bool IsMatch(object target)
		{
			return ReferenceEquals(target, null);
		}

		#endregion

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return true;
			}

			if (obj is UndefinedValidationTarget)
			{
				return true;
			}

			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}
	}
}