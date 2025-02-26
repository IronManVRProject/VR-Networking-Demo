using System.Diagnostics;
using Unity.Netcode;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RpcTest : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            TestServerRpc(0);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    void TestClientRpc(int value)
    {
        if (IsClient)
        {
            Debug.Log("Client received the RPC #" + value);
            TestServerRpc(value + 1);
        }
    }

    [Rpc(SendTo.Server)]
    void TestServerRpc(int value)
    {
        Debug.Log("Server received the RPC #" + value);
        TestClientRpc(value);
    }
}
