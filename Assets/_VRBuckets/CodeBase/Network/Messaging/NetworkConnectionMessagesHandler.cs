using System;
using System.Collections.Generic;
using _VRBuckets.CodeBase.Logging;
using _VRBuckets.CodeBase.Network.Messaging.NetworkEvents;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace _VRBuckets.CodeBase.Network.Messaging
{
    public class NetworkConnectionMessagesHandler : INetworkRunnerCallbacks
    {
        private readonly INetworkEventBus _networkEventBus;

        public NetworkConnectionMessagesHandler(INetworkEventBus networkEventBus)
        {
            _networkEventBus = networkEventBus;
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            _networkEventBus.Publish(new PlayerJoinedEvent(player));
            AppLogger.Log(LogCategory.Network, "Player joined " + player.PlayerId);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            AppLogger.Log(LogCategory.Network, "Player left " + player.PlayerId);
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            AppLogger.Log(LogCategory.Network, "Shutdown " + shutdownReason);
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            AppLogger.Log(LogCategory.Network, "Disconnected from server " + reason);
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            AppLogger.Log(LogCategory.Network, "Connected to server");
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            AppLogger.LogError(LogCategory.Network, "Connect failed " + reason);
        }

        #region Nut used

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

        public void OnInput(NetworkRunner runner, NetworkInput input) { }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            Debug.Log("Sessions list updated, count: " + sessionList.Count);
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            Debug.Log("scene loaded");
        }

        public void OnSceneLoadStart(NetworkRunner runner) { }

        #endregion
    }
}