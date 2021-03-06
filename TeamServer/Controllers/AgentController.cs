﻿using System;
using System.Linq;
using System.Collections.Generic;

using C2.Models;

using TeamServer.Models;

namespace TeamServer.Controllers
{
    public class AgentController
    {
        internal List<SessionData> sessionCache = new List<SessionData>();
        internal List<AgentEvent> agentEvents = new List<AgentEvent>();

        internal static AgentController Create()
        {
            return new AgentController();
        }

        internal void CreateSession(InitialMetadata data)
        {
            sessionCache.Add(new SessionData
            {
                AgentId = data.AgentId,
                ParentAgentId = data.ParentAgentId,
                InternalAddresses = data.InternalAddresses,
                ComputerName = data.ComputerName,
                Identity = data.Identity,
                ProcessName = data.ProcessName,
                ProcessId = data.ProcessId,
                Architecture = data.Architecture,
                Integrity = data.Integrity,
                FirstSeen = DateTime.UtcNow
            });
        }

        internal SessionData GetSession(string agentId)
        {
            var session = sessionCache.Where(s => s.AgentId.Equals(agentId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (session == null)
                return new SessionData();
            else
                return session;
        }

        internal void UpdateSession(string agentId, SessionData data)
        {
            if (string.IsNullOrEmpty(agentId)) { return; }

            var session = sessionCache.Where(s => s.AgentId.Equals(agentId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            session = data;
        }

        internal AgentEvent[] GetAgentEvents(string agentId)
        {
            return agentEvents.Where(e => e.AgentId.Equals(agentId, StringComparison.OrdinalIgnoreCase)).OrderBy(e => e.EventTime).ToArray();
        }
    }
}