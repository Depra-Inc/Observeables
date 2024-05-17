// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Threading.Tasks;

namespace Depra.Expectation
{
	public static class ExpectantExtensions
	{
		public static void Subscribe(this IExpectant self, Action callback) => self.Subscribe(callback, callback);

		public static void Unsubscribe(this IExpectant self, Action callback) => self.Unsubscribe(callback);

		/// <summary>
		/// Extension method that converts an <see cref="IExpectant"/> into a <see cref="Task"/>.
		/// </summary>
		/// <param name="self">The <see cref="IExpectant"/> to convert.</param>
		/// <returns>A Task that represents the completion of the <see cref="IExpectant"/>.</returns>
		public static Task AsTask(this IExpectant self)
		{
			var tcs = new TaskCompletionSource<bool>();
			self.Subscribe(() => tcs.SetResult(true));

			return tcs.Task;
		}
	}
}