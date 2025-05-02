using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UserTcpClient : MonoBehaviour
{
    private const string SERVER_IP = "127.0.0.1"; // Rust에서 사용한 주소로 교체
    private const int SERVER_PORT = 8080;        // 실제 포트로 교체

    private async void Start()
    {
        await UuidTcpClient();
    }

    private async Task UuidTcpClient()
    {
        // 1차 요청: UUID 생성 요청
        string uid;
        using (TcpClient client = new TcpClient())
        {
            await client.ConnectAsync(SERVER_IP, SERVER_PORT);
            NetworkStream stream = client.GetStream();

            string name = "test"; // UUID를 생성할 이름
            string msg = $"NEW_UUID.{name}";
            byte[] request = Encoding.UTF8.GetBytes(msg);
            await stream.WriteAsync(request, 0, request.Length);

            byte[] buffer = new byte[512];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            uid = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

            Debug.Log($"uid: {uid}");
        }
        // 2차 요청: UUID로 연결 시도
        using (TcpClient client = new TcpClient())
        {
            await client.ConnectAsync(SERVER_IP, SERVER_PORT);
            NetworkStream stream = client.GetStream();

            string data = "test data"; // 전달할 데이터
            string msg = $"CONNECT_UUID.{uid}.{data}";
            byte[] connectMsg = Encoding.UTF8.GetBytes(msg);
            await stream.WriteAsync(connectMsg, 0, connectMsg.Length);

            byte[] buffer = new byte[512];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Debug.Log($"Server connect: {response}");
        }
    }
}
