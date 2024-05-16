﻿// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Expectation.Exceptions;

namespace Depra.Expectation.UnitTests;

public sealed class OrExpectantTests
{
	[Fact]
	public void Build_ShouldThrowsExpectantAlreadyBuilt_WhenExpectantIsAlreadyBuilt()
	{
		// Arrange:
		var groupExpectant = new GroupExpectant.Or();
		groupExpectant.Build();

		// Act:
		var act = () => groupExpectant.Build();

		// Assert:
		act.Should().Throw<ExpectantAlreadyBuilt>();
	}

	[Fact]
	public void Build_ShouldReturnExpectant_WhenExpectantIsNotBuilt()
	{
		// Arrange:
		var groupExpectant = new GroupExpectant.Or();

		// Act:
		var expectant = groupExpectant.Build();

		// Assert:
		expectant.Should().NotBeNull();
	}

	[Fact]
	public void SetReady_ShouldSetReady_WhenAnyExpectantAreReady()
	{
		// Arrange:
		var expectant1 = new Expectant();
		var expectant2 = new Expectant();
		var groupExpectant = new GroupExpectant.Or();
		groupExpectant.With(expectant1);
		groupExpectant.With(expectant2);
		var builtExpectant = groupExpectant.Build();

		// Act:
		expectant1.SetReady();

		// Assert:
		builtExpectant.IsReady().Should().BeTrue();
	}

	[Fact]
	public void With_ShouldThrowsExpectantAlreadyBuilt_WhenExpectantIsAlreadyBuilt()
	{
		// Arrange:
		var expectant = new Expectant();
		var groupExpectant = new GroupExpectant.Or();
		groupExpectant.Build();

		// Act:
		var act = () => groupExpectant.With(expectant);

		// Assert:
		act.Should().Throw<ExpectantAlreadyBuilt>();
	}
}