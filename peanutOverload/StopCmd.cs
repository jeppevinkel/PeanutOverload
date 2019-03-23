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
    class StopCmd : ICommandHandler
    {
        private PeanutOverload plugin;

        public StopCmd(PeanutOverload plugin)
        {
            //Constructor passing plugin refrence to this class
            this.plugin = plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP HELLO
            return "Stops the PeanutOverload event";
        }

        public string GetUsage()
        {
            return "po_stop";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (!plugin.isActive) return new string[] { "PeanutOverlaod is not active!" };

            plugin.isActive = false;

            plugin.Server.Map.Broadcast(10, "PO event stopped!", false);
            return new string[] { "PeanutOverload Deactivated" };
        }
    }
}