using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Mvvm.Validation.Internal
{
	internal class PropertyValidationTarget : IValidationTarget, IEquatable<PropertyValidationTarget>
	{
		public PropertyValidationTarget(Expression<Func<object>> propertyNameExpression)
		{
            PropertyNameExpression = propertyNameExpression;
            PropertyName = Common.PropertyName.For(propertyNameExpression);
            Contract.Requires(!string.IsNullOrEmpty(PropertyName));
		}

		

		private string PropertyName { get; set; }
        private Expression<Func<object>> PropertyNameExpression { get; set; }

		#region IEquatable<PropertyValidationTarget> Members

		public bool Equals(PropertyValidationTarget other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}
			return Equals(other.PropertyName, PropertyName);
		}

		#endregion

		#region IValidationTarget Members

		public IEnumerable<object> UnwrapTargets()
		{
			return new[] {PropertyName};
		}

        public IEnumerable<Expression<Func<object>>> UnwrapExpressions()
        {
            return new[] { PropertyNameExpression };
        }

		public bool IsMatch(object target)
		{
			return Equals(PropertyName, target);
		}

		#endregion

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != typeof(PropertyValidationTarget))
			{
				return false;
			}
			return Equals((PropertyValidationTarget)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (PropertyName != null ? PropertyName.GetHashCode() : 0);
			}
		}
	}
}