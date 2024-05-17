// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using System.Linq;
using Depra.Expectation.Exceptions;

namespace Depra.Expectation
{
	public static class GroupExpectant
	{
		public abstract class Abstract<T> : IGroupExpectant where T : Abstract<T>
		{
			private readonly Expectant _ready = new();

			private bool _built;

			public void Dispose() => _ready.Dispose();

			protected HashSet<IExpectant> Expectants { get; } = new();

			public T With(IExpectant expectant) => (T) ((IGroupExpectant) this).With(expectant);

			public IExpectant Build()
			{
				Guard.Against(_built, () => new ExpectantAlreadyBuilt());

				_built = true;
				TryRefresh();

				return this;
			}

			protected abstract void Refresh();

			protected void SetReady()
			{
				foreach (var expectant in Expectants)
				{
					expectant.Unsubscribe(TryRefresh);
				}

				Expectants.Clear();
				_ready.SetReady();
			}

			private void TryRefresh()
			{
				if (_built)
				{
					Refresh();
				}
			}

			bool IExpectant.IsReady() => _ready.IsReady();

			void IExpectant.Subscribe(object key, Action callback) => _ready.Subscribe(key, callback);

			void IExpectant.Unsubscribe(object key) => _ready.Unsubscribe(key);

			IGroupExpectant IGroupExpectant.With(IExpectant expectant)
			{
				Guard.Against(_built, () => new ExpectantAlreadyBuilt());
				Guard.Against(_ready.IsReady(), () => new ExpectantAlreadyCompleted());

				if (Expectants.Add(expectant) == false)
				{
					return this;
				}

				if (expectant.IsReady() == false)
				{
					expectant.Subscribe(TryRefresh);
				}

				return this;
			}
		}

		public sealed class And : Abstract<And>
		{
			private HashSet<IExpectant> _completed = new();

			protected override void Refresh()
			{
				foreach (var expectant in Expectants)
				{
					if (expectant.IsReady() == false)
					{
						if (_completed is not { Count: > 0 })
						{
							return;
						}

						foreach (var completed in _completed)
						{
							Expectants.Remove(completed);
						}

						_completed.Clear();
						return;
					}

					_completed?.Add(expectant);
				}

				if (_completed != null)
				{
					_completed.Clear();
					_completed = null;
				}

				SetReady();
			}
		}

		public sealed class Or : Abstract<Or>
		{
			protected override void Refresh()
			{
				if (Expectants.Any(expectant => expectant.IsReady()))
				{
					SetReady();
				}
			}
		}
	}
}