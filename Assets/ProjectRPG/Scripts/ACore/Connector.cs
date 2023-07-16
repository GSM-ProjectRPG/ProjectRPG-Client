using System;
using System.Net;
using System.Net.Sockets;

namespace ACore
{
	public class Connector
	{
		private Func<Session> _sessionFactory;

		public void Connect(IPEndPoint endPoint, Func<Session> sessionFactory, int count = 1)
		{
			for (int i = 0; i < count; i++)
			{
				Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				socket.NoDelay = true;
				_sessionFactory = sessionFactory;

				SocketAsyncEventArgs args = new SocketAsyncEventArgs();
				args.Completed += OnConnectCompleted;
				args.RemoteEndPoint = endPoint;
				args.UserToken = socket;

				RegisterConnect(args);
			}
		}

		private void RegisterConnect(SocketAsyncEventArgs args)
		{
			Socket socket = args.UserToken as Socket;
			if (socket == null)
				return;

			try
			{
				bool pending = socket.ConnectAsync(args);
				if (pending == false)
					OnConnectCompleted(null, args);
			}
			catch (Exception e)
			{
                Console.WriteLine($"RegisterConnect Failed {e}");
            }
		}

		private void OnConnectCompleted(object sender, SocketAsyncEventArgs args)
		{
			if (args.SocketError == SocketError.Success)
			{
				Session session = _sessionFactory.Invoke();
				session.Start(args.ConnectSocket);
				session.OnConnected(args.RemoteEndPoint);
			}
			else
			{
				Console.WriteLine($"OnConnectCompleted Fail: {args.SocketError}");
			}
		}
	}
}