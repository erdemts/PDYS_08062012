using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Mvvm.Validation
{
	/// <summary>
	/// Contains arguments for the <see cref="ValidationHelper.ResultChanged"/> event.
	/// </summary>
	public class ValidationResultChangedEventArgs : EventArgs
	{
        internal ValidationResultChangedEventArgs(object target, Expression<Func<object>> expression, ValidationResult newResult)
		{
			Contract.Requires(newResult != null);

			this.Target = target;
			this.NewResult = newResult;
            this.Expression = expression;
		}

		/// <summary>
		/// Gets the target, for which the validation result has changed.
		/// </summary>
		public object Target { get; private set; }
        public Expression<Func<object>> Expression { get; private set; }

		/// <summary>
		/// Gets the new validation result.
		/// </summary>
		public ValidationResult NewResult { get; private set; }
	}
}