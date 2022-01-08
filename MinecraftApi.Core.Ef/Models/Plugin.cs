using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Api.Models;
using MinecraftApi.Core.Ef.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Ef.Models
{
    /// <summary>
    /// Plugin base class.
    /// </summary>
    public class Plugin : IEntity, IPlugin
    {
        /// <inheritdoc/>
        public long Id { get; set; }
        /// <inheritdoc/>
        public string? Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        /// <summary>
        /// Commands for EF Core.
        /// </summary>
        public IList<Command>? Commands { get; set; }

        [NotMapped]
        IList<ICommand>? IPlugin.Commands
        {
            get => (IList<ICommand>?)Commands;
            set => Commands = value as IList<Command>;
        }
    }
}
