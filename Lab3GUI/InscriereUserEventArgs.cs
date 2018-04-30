using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3GUI
{
    public enum InscriereUserEvent
    {
        NewInscriere
    }
    public class InscriereUserEventArgs:EventArgs
    {
        private readonly InscriereUserEvent userEvent;
        private readonly Object data;
        public InscriereUserEventArgs(InscriereUserEvent userEvent, object data)
        {
            this.userEvent = userEvent;
            this.data = data;
        }
        public InscriereUserEvent UserEventType
        {
            get { return userEvent; }
        }

        public object Data
        {
            get { return data; }
        }

    }
}
