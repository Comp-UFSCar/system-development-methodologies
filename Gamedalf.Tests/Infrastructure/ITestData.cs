using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;

namespace Gamedalf.Tests.Infrastructure
{
    interface ITestData<T>
        where T : class
    {
        ICollection<T> Data { get; set; }
    }
}
