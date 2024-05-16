// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Expectation.Exceptions
{
	internal sealed class ExpectantAlreadyCompleted : Exception
	{
		public ExpectantAlreadyCompleted() : base("Expectant is already completed!") { }
	}
}