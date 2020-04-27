namespace _05._Singleton
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The 'Singleton' class
    /// </summary>
    public class LoadBalancer
    {
        private static LoadBalancer _instance;
        private readonly List<string> _servers;
        private readonly Random _random;

        // Lock synchronization object
        private static readonly object _syncLock = new object();

        // Constructor (protected)
        protected LoadBalancer()
        {
            this._servers = new List<string>();
            this._random = new Random();

            // List of available servers
            this._servers.Add("ServerI");
            this._servers.Add("ServerII");
            this._servers.Add("ServerIII");
            this._servers.Add("ServerIV");
            this._servers.Add("ServerV");
        }

        public static LoadBalancer GetLoadBalancer()
        {
            // Support multi threaded applications through

            // 'Double checked locking' pattern which (once

            // the instance exists) avoids locking each

            // time the method is invoked

            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new LoadBalancer();
                    }
                }
            }

            return _instance;
        }

        // Simple, but effective random load balancer
        public string Server => this._servers[this._random.Next(this._servers.Count)];
    }
}
