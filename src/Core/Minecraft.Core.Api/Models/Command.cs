using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Api.Models
{
    ///<inheritdoc/>
    public class BaseCommand : ICommand
    {
        ///<inheritdoc/>
        public string? Name { get; set; }
        ///<inheritdoc/>
        public string? Description { get; set; }
        ///<inheritdoc/>
        public string? Prefix { get; set; }
        ///<inheritdoc/>
        public IList<IArgument>? Arguments { get; set; }
    }
}
