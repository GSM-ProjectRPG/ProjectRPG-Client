using System;
using System.Net;
using System.Net.Sockets;

namespace ACore
{
	public class Listener
	{
		private Socket _listenSocket;
		private Func<Session> _sessionFactory;

		public void Init(IPEndPoint endPoint, Func<Session> sessionFactory, int register = 10, int backlog = 100)
		{
			_listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			_listenSocket.NoDelay = true;
			_sessionFactory += sessionFactory;

			_listenSocket.Bind(endPoint);
			_listenSocket.Listen(backlog);

			for (int i = 0; i < register; i++)
			{
				SocketAsyncEventArgs args = new SocketAsyncEventArgs();
				args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
				RegisterAccept(args);
			}
		}

		private void RegisterAccept(SocketAsyncEventArgs args)
		{
			args.AcceptSocket = null;

			try
			{
				bool pending = _listenSocket.AcceptAsync(args);
				if (pending == false)
					OnAcceptCompleted(null, args);
			}
			catch (Exception e)
			{
                Console.WriteLine($"RegisterAccept Failed {e}");
            }
		}

		private void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
		{
			if (args.SocketError == SocketError.Success)
			{
				Session session = _sessionFactory.Invoke();
				session.Start(args.AcceptSocket);
				session.OnConnected(args.AcceptSocket.RemoteEndPoint);
			}
			else
			{
				Console.WriteLine($"OnAcceptCompleted Failed {args.SocketError}");
			}

			RegisterAccept(args);
		}
	}
}