// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Expectation.UnitTests;

public sealed class ExpectantTests
{
	[Fact]
	public void Completed_ShouldBeReady()
	{
		// Arrange:
		var expectant = Expectant.COMPLETED;

		// Act:
		var isReady = expectant.IsReady();

		// Assert:
		isReady.Should().BeTrue();
	}

	[Fact]
	public void Uncompleted_ShouldNotBeReady()
	{
		// Arrange:
		var expectant = Expectant.UNCOMPLETED;

		// Act:
		var isReady = expectant.IsReady();

		// Assert:
		isReady.Should().BeFalse();
	}

	[Fact]
	public void Disposed_ShouldNotBeReady()
	{
		// Arrange:
		var expectant = new Expectant();
		expectant.Subscribe(new object(), () => { });

		// Act:
		expectant.Dispose();

		// Assert:
		expectant.IsReady().Should().BeFalse();
	}

	[Fact]
	public void SetReady_WhenSubscribed_ShouldInvokeCallback()
	{
		// Arrange:
		var expectant = new Expectant();
		var invoked = false;
		expectant.Subscribe(new object(), () => invoked = true);

		// Act:
		expectant.SetReady();

		// Assert:
		invoked.Should().BeTrue();
	}

	[Fact]
	public void SetReady_WhenUnsubscribed_ShouldNotInvokeCallback()
	{
		// Arrange:
		var expectant = new Expectant();
		var key = new object();
		var invoked = false;
		expectant.Subscribe(key, () => invoked = true);
		expectant.Unsubscribe(key);

		// Act:
		expectant.SetReady();

		// Assert:
		invoked.Should().BeFalse();
	}

	[Fact]
	public void SetReady_WhenDisposed_ShouldNotInvokeCallback()
	{
		// Arrange:
		var expectant = new Expectant();
		var key = new object();
		var invoked = false;
		expectant.Subscribe(key, () => invoked = true);
		expectant.Dispose();

		// Act:
		expectant.SetReady();

		// Assert:
		invoked.Should().BeFalse();
	}

	[Fact]
	public void Dispose_WhenReady_ShouldNotThrow()
	{
		// Arrange:
		var expectant = new Expectant();
		expectant.SetReady();

		// Act:
		var act = () => expectant.Dispose();

		// Assert:
		act.Should().NotThrow();
	}
}