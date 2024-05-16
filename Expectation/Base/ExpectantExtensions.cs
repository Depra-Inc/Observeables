// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Expectation
{
	public static class ExpectantExtensions
	{
		public static void Subscribe(this IExpectant self, Action callback) =>
			self.Subscribe(callback, callback);

		public static void Unsubscribe(this IExpectant self, Action callback) =>
			self.Unsubscribe(callback);
	}
}