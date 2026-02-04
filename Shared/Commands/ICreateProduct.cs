using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Commands
{
    public interface ICreateProduct
    {
        string ?TitleId { get; }
        string ?Title1 { get; }
        string ?Type { get; }
        decimal?Price { get; }
        string ?Notes { get; }
        DateTime Pubdate { get; }
    }
}
