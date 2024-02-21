// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Observables
{
	public interface IReactive
	{
		event Action Changed;
	}
}