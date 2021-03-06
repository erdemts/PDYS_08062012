using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Mvvm.Validation.Internal
{
	[ContractClassFor(typeof(IValidationTarget))]
	// ReSharper disable InconsistentNaming
	internal abstract class IValidationTargetContract : IValidationTarget
		// ReSharper restore InconsistentNaming
	{
		#region IValidationTarget Members

		public IEnumerable<object> UnwrapTargets()
		{
			Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);

			throw new NotImplementedException();
		}

        public IEnumerable<Expression<Func<object>>> UnwrapExpressions()
        {
            Contract.Ensures(Contract.Result<IEnumerable<Expression<Func<object>>>>() != null);

            throw new NotImplementedException();
        }

		public bool IsMatch(object target)
		{
			Contract.Requires(target != null);

			throw new NotImplementedException();
		}

		#endregion

		public void NotifyValidtionCompleted(ValidationResult validationResult)
		{
			Contract.Requires(validationResult != null);

			throw new NotImplementedException();
		}
	}
}