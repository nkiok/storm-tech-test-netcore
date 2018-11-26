using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Services
{
    public interface IGravatarService
    {
        string GetImgUrl(string emailAddress);
    }
}
