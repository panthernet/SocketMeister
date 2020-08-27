﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace SocketMeister.Testing
{
    /// <summary>
    /// Test Harness Client
    /// </summary>
    internal class ClientController
    {
        private readonly ControlBusClient _controlBusClient;
        private readonly object _lock = new object();

        /// <summary>
        /// Triggered when connection could not be established with the HarnessController. This ClientController should now abort (close)
        /// </summary> 
        public event EventHandler<EventArgs> ControlBusConnectionFailed;

        public ClientController(int ControlBusClientId, string ControlBusServerIPAddress)
        {
            _controlBusClient = new ControlBusClient( ControlBusClientType.ClientController, ControlBusClientId, ControlBusServerIPAddress, Constants.HarnessControlBusPort);
            _controlBusClient.ConnectionFailed += ControlBus_ConnectionFailed;
            _controlBusClient.ControlBusSocketClient.MessageReceived += ControlBus_MessageReceived;
        }

        private void ControlBus_MessageReceived(object sender, SocketClient.MessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ControlBus_ConnectionFailed(object sender, EventArgs e)
        {
            //  CONNECTION TO THE HarnessController COULD NOT BE ESTABLISHED.
            ControlBusConnectionFailed?.Invoke(this, e);
        }


        public void Stop()
        {
            _controlBusClient.Stop();
        }

    }
}
