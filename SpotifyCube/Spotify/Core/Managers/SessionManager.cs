using System;
using System.Collections.Generic;
using Spotify.Core.Native;

namespace Spotify.Core.Managers
{
    internal class SessionManager
    {
        #region Fields

        private static Dictionary<IntPtr, NativeSession> _sessions = new Dictionary<IntPtr, NativeSession>();

        #endregion Fields

        #region Internal Static Methods

        internal static ISession Create(
            byte[] applicationKey, 
            SessionOptions options)
        {
            NativeSession session = new NativeSession(applicationKey, options);

            session.Initialize();
            
            if (SessionFactory.IsInternalCachingEnabled)
            {
                _sessions.Add(session.Handle, session);
            }

            return session;
        }

        internal static NativeSession Get(IntPtr sessionPtr)
        {
            NativeSession s;
            _sessions.TryGetValue(sessionPtr, out s);
            return s;
        }

        internal static void Remove(IntPtr sessionPtr)
        {
            if (_sessions.ContainsKey(sessionPtr))
            {
                _sessions.Remove(sessionPtr);
            }
        }

        #endregion Internal Static Methods
    }
}