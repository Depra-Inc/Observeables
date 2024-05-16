// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Expectation
{
	public interface IExpectant : IDisposable
	{
		bool IsReady();

		void Subscribe(object key, Action callback);

		void Unsubscribe(object key);
	}
}