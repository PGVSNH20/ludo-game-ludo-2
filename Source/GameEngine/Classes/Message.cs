using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public class Message
    {
        //public static string StatusMessage { get; set; }
        //public static string ActionMessage { get; set; }

        public string StatusMessage { get; set; }
        public string ActionMessage { get; set; }

        //public Message(string status, string action)
        //{
        //    StatusMessage = status;
        //    ActionMessage = action;
        //}

        public  void SetActionMessage(string message) => ActionMessage = message;

        public  void SetStatusMessage(string message) => StatusMessage = message;
        //public static void SetActionMessage(string message) => ActionMessage = message;

        //public static void SetStatusMessage(string message) => StatusMessage = message;
    }
}
