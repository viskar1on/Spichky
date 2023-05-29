using MatchGame.Commands.Base;
using MatchGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGame.Commands
{
    internal class AcceptCommand : Command
    {
        public override void Execute(object parametr)
        {
            //object[] param = parameters;
            GameViewModel gvm = parametr as GameViewModel;
            gvm.Accept();
        }
    }
}
