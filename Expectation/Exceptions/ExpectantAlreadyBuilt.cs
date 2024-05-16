// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Expectation.Exceptions
{
	internal sealed class ExpectantAlreadyBuilt : Exception
	{
		public ExpectantAlreadyBuilt() : base("Expectant is already built!") { }
	}
}