using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EffectTests
{ 
    CatScratchFever sut;

    const int DURATION = 6;
    const int ZERO = 0;
    const int ONE = 1;
    const int TWO = 2;

    [SetUp]
    public void Init()
    {
        sut = new CatScratchFever();
    }

    [Test]
    public void Is_duration_over_returns_if_duration_is_over()
    {
        Assert.That(sut.IsDurationOver(), Is.EqualTo(false));

        for (int i = 0; i < DURATION; i++)
        {
            sut.DecrementDuration();
        }

        Assert.That(sut.IsDurationOver(), Is.EqualTo(true));
    }

    [Test]
    public void Decrement_duration_decrements_duration_remaining()
    {
        // Reduce duration to 1
        for (int i = 0; i < DURATION - 1; i++)
        {
            sut.DecrementDuration();
        }

        Assert.That(sut.IsDurationOver(), Is.EqualTo(false));

        sut.DecrementDuration();

        Assert.That(sut.IsDurationOver(), Is.EqualTo(true));
    }

    [Test]
    public void Decrement_stack_decrements_stacks()
    {
        Assert.That(sut.Stacks, Is.EqualTo(ONE));

        sut.DecrementStack();

        Assert.That(sut.Stacks, Is.EqualTo(ZERO));
    }

    [Test]
    public void Stacks_ran_out_returns_true_if_stacks_are_zero_or_lower()
    {
        sut.Stacks = 0;

        Assert.That(sut.StacksRanOut(), Is.EqualTo(true));
    }

    [Test]
    public void Refresh_increases_stacks_and_sets_duration_to_default()
    {
        Assert.That(sut.Stacks, Is.EqualTo(ONE));
        for (int i = 0; i < DURATION; i++)
        {
            sut.DecrementDuration();
        }
        Assert.That(sut.IsDurationOver(), Is.EqualTo(true));

        sut.Refresh();

        Assert.That(sut.IsDurationOver(), Is.EqualTo(false));
        Assert.That(sut.Stacks, Is.EqualTo(TWO));
    }
}
