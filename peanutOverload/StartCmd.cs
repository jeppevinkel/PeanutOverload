using Smod2.API;
using Smod2.Commands;
using Smod2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace peanutOverload
{
    class StartCmd : ICommandHandler
    {
        private readonly PeanutOverload plugin;

        static IConfigFile Config => ConfigManager.Manager.Config;

        public StartCmd(PeanutOverload plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin = plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Starts the event";
        }

        public string GetUsage()
        {
            return "po_start";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (plugin.isActive) return new string[] { "PeanutOverlaod is already active!" };

            plugin.isActive = true;

            return new string[] { "PeanutOverload", "Activated" };
        }
    }
}