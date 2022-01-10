using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models
{
    /// <summary>
    /// Generic Command
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Command<T> : ICommandEntity<T> 
        where T : IArgumentEntity
    {
        /// <summary>
        /// Arguments associated with the object
        /// </summary>
        public IList<T>? Arguments { get; set; }
        /// <summary>
        /// Associated plugin. Many commands - one plugin relationship.
        /// </summary>
        [JsonIgnore]
        public Plugin? Plugin { get; set; }
        /// <summary>
        /// Associated plugin Id. Many commands - one plugin relationship.
        /// </summary>
        public long PluginId { get; set; }
        /// <inheritdoc/>
        public string? Name { get; set; }
        /// <inheritdoc/>
        public string? Description { get; set; }
        /// <inheritdoc/>
        public string Prefix { get; set; } = String.Empty;
        /// <inheritdoc/>
        public long Id { get; set; }

        /// The commands are formattable:
        /// Format supports p = plain, j = json, d = describe
        ///  <inheritdoc/>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (String.IsNullOrEmpty(format)) format = "p";
            if (formatProvider == null) formatProvider = CultureInfo.CurrentCulture;
            switch (format)
            {
                case "p":
                    return this.ToString();
                case "d":
                    return Describe();
            }
            throw new ArgumentException("Format parameter not accepted {0}", format);
        }
        ///<inheritdoc/>
        public string Describe()
        {
            var arguments = Arguments?.Select(x => x.ToString()).ToList();
            if (arguments != null && arguments.Any())
            {
                return $"{Name} ({Description}): {Prefix} {string.Join(" ", arguments)}";
            }
            return $"{Name} ({Description}): {Prefix}";
        }
        /// <summary>
        /// Converts the command to its string representation as prefix + arguments (if it has arguments).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var arguments = Arguments?.Select(x => x.ToString()).ToList();
            if (arguments != null && arguments.Any())
            {
                return $"{Prefix} {string.Join(" ", arguments)}";
            }
            return $"{Prefix}";
        }
    }
    /// <summary>
    /// Command class for EF core.
    /// </summary>
    public class Command : Command<Argument>,  ICommandEntity<Argument>
    {
        /// <summary>
        /// 
        /// </summary>
        public Command() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public Command(ICommandEntity<Argument> command)
        {
            Name = command.Name;
            Description = command.Description;
            Prefix = command.Prefix;
            Arguments = command.Arguments?.Select(x => new Argument(x)).ToList();
        }
    }
}
