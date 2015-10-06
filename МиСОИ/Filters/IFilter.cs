using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filters
{
    public interface IFilter
    {
        Color[][] Apply(Color[][] colors);
    }
}
