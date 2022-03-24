using System;
using System.Collections.Generic;

namespace DTOSearchExpressionBuilder.Test;

public class EntitiesFixture : IDisposable
{
    public List<Student> StudentsEntitiesSample { get; private set; }

    public EntitiesFixture()
    {
        StudentsEntitiesSample = new List<Student>
        {
            new() { Id=10, Name="Hasan" , Age=18, Description="Test 12"},
            new() { Id=11, Name="Mehdi", Age=16, Description="Test 9"},
            new() { Id=10, Name="Ali", Age=13, Description="Test 8"},
            new() { Id=12, Name="Javad", Age=28, Description="Test 13"},
        };
    }

    public void Dispose()
    {
        StudentsEntitiesSample.Clear();
    }

}