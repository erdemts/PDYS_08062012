using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mvvm.Validation
{
	/// <summary>
	/// Represents a collection of <see cref="ValidationError"/> instances.
	/// </summary>
	public class ValidationErrorCollection : Collection<ValidationError>
	{
		internal ValidationErrorCollection()
		{
		}

		internal ValidationErrorCollection(IList<ValidationError> list)
			: base(list)
		{
		}
	}
}