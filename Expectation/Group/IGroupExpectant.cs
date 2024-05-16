// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Expectation
{
	public interface IGroupExpectant : IExpectant
	{
		IGroupExpectant With(IExpectant expectant);
	}
}