using MinecraftApi.Core.Api.Contracts.Models;
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
    /// Command class for EF core.
    /// </summary>
    public class Command : IEntity, ICommand
    {
        /// <inheritdoc/>
        public long Id { get; set; }
        /// <inheritdoc/>
        public string? Name { get; set; }
        /// <inheritdoc/>
        public string? Description { get; set; }
        /// <inheritdoc/>
        public string? Prefix { get; set; }

        /// <summary>
        /// Arguments for the EF Core.
        /// </summary>
        public IList<Argument>? Arguments { get; set; }

        [NotMapped]
        IList<IArgument>? ICommand.Arguments
        {
            get { return (IList<IArgument>?)Arguments; }
            set { Arguments = value as IList<Argument>; }
        }
    }
}
