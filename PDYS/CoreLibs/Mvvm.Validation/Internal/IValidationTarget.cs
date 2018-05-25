using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Mvvm.Validation.Internal
{
	[ContractClass(typeof(IValidationTargetContract))]
	internal interface IValidationTarget
	{
		IEnumerable<object> UnwrapTargets();

        IEnumerable<Expression<Func<object>>> UnwrapExpressions();

		bool IsMatch(object target);
	}
}