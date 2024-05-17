// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Depra.Expectation
{
	[DebuggerDisplay("State: {IsReady()}")]
	public sealed class Expectant : IExpectant
	{
		public static readonly IExpectant COMPLETED = new CompletedExpectant();
		public static readonly IExpectant UNCOMPLETED = new UncompletedExpectant();

		private bool _disposed;
		private HashSet<object> _unsubscribed = new();
		private Dictionary<object, Action> _listeners = new();

		public void Dispose()
		{
			if (_disposed)
			{
				return;
			}

			_listeners?.Clear();
			_disposed = true;
		}

		public bool IsReady() => _listeners == null && !_disposed;

		public void SetReady()
		{
			if (_listeners == null || _disposed)
			{
				return;
			}

			var listeners = _listeners;
			_listeners = null;
			_unsubscribed = new HashSet<object>();

			foreach (var (_, action) in listeners.Where(pair => _unsubscribed.Contains(pair.Key) == false))
			{
				action.Invoke();
			}

			_unsubscribed.Clear();
			_unsubscribed = null;
		}

		public void Subscribe(object key, Action callback)
		{
			if (_listeners != null)
			{
				_listeners[key] = callback;
			}
			else if (_disposed == false)
			{
				callback.Invoke();
			}
		}

		public void Unsubscribe(object key)
		{
			if (_disposed)
			{
				return;
			}

			if (_listeners == null)
			{
				_unsubscribed?.Add(key);
			}
			else
			{
				_listeners.Remove(key);
			}
		}

		private sealed class CompletedExpectant : IExpectant
		{
			void IDisposable.Dispose() { }

			bool IExpectant.IsReady() => true;

			void IExpectant.Subscribe(object key, Action callback) { }

			void IExpectant.Unsubscribe(object key) { }
		}

		private sealed class UncompletedExpectant : IExpectant
		{
			bool IExpectant.IsReady() => false;

			void IExpectant.Subscribe(object key, Action callback) { }

			void IExpectant.Unsubscribe(object key) { }

			void IDisposable.Dispose() { }
		}
	}
}