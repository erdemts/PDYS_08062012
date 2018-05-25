using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Mvvm.Validation.Internal
{
	internal class PropertyCollectionValidationTarget : IValidationTarget
	{
		public PropertyCollectionValidationTarget(IEnumerable<Expression<Func<object>>> properties)
		{
            Contract.Requires(properties != null);
            Contract.Requires(properties.Any());

            Properties = properties.Select(p => Common.PropertyName.For(p));

		}

        private IEnumerable<string> Properties { get; set; }
        private IEnumerable<Expression<Func<object>>> PropertyNameExpression { get; set; }

		#region IValidationTarget Members

		public IEnumerable<object> UnwrapTargets()
		{
			return Properties.ToArray();
		}

        public IEnumerable<Expression<Func<object>>> UnwrapExpressions()
        {
            return PropertyNameExpression;
        }

		public bool IsMatch(object target)
		{
			return Properties.Any(p => Equals(p, target));
		}

		#endregion
	}
}