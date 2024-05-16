// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Reactive
{
	public sealed class ReactiveProperty<TValue> : IReactiveProperty<TValue>
	{
		private TValue _value;

		public event Action<TValue> Changed;

		public void Dispose()
		{
			Changed = null;
			_value = default;
		}

		public TValue Value
		{
			get => _value;
			set
			{
				if (!_value.Equals(value))
				{
					Changed?.Invoke(_value = value);
				}
			}
		}
	}
}