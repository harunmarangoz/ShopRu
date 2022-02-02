﻿namespace Core.Tests;

[AttributeUsage(AttributeTargets.Method)]
public class TestPriorityAttribute : Attribute
{
    public int Priority { get; private set; }

    public TestPriorityAttribute(int priority) => Priority = priority;
}