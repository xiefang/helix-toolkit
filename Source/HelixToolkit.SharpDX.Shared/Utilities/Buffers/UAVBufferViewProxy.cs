﻿/*
The MIT License (MIT)
Copyright (c) 2018 Helix Toolkit contributors
*/
using SharpDX.Direct3D11;
using System;
using System.Diagnostics.CodeAnalysis;
using SDX11 = SharpDX.Direct3D11;
#if !NETFX_CORE
namespace HelixToolkit.Wpf.SharpDX.Utilities
#else
namespace HelixToolkit.UWP.Utilities
#endif
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UAVBufferViewProxy : IDisposable
    {
        private SDX11.Buffer buffer;
        /// <summary>
        /// The uav
        /// </summary>
        public UnorderedAccessView uav;
        /// <summary>
        /// The SRV
        /// </summary>
        public ShaderResourceView srv;

        /// <summary>
        /// Get UnorderedAccessView
        /// </summary>
        public UnorderedAccessView UAV { get { return uav; } }

        /// <summary>
        /// Get ShaderResourceView
        /// </summary>
        public ShaderResourceView SRV { get { return srv; } }
        /// <summary>
        /// Initializes a new instance of the <see cref="UAVBufferViewProxy"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="bufferDesc">The buffer desc.</param>
        /// <param name="uavDesc">The uav desc.</param>
        /// <param name="srvDesc">The SRV desc.</param>
        public UAVBufferViewProxy(Device device, ref BufferDescription bufferDesc, ref UnorderedAccessViewDescription uavDesc, ref ShaderResourceViewDescription srvDesc)
        {
            buffer = new SDX11.Buffer(device, bufferDesc);
            srv = new ShaderResourceView(device, buffer);
            uav = new UnorderedAccessView(device, buffer, uavDesc);
        }
        /// <summary>
        /// Copies the count.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="offset">The offset.</param>
        public void CopyCount(DeviceContext device, SDX11.Buffer destBuffer, int offset)
        {
            device.CopyStructureCount(destBuffer, offset, UAV);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        [SuppressMessage("Microsoft.Usage", "CA2213: Disposable fields should be disposed", Justification = "False positive.")]
        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Disposer.RemoveAndDispose(ref uav);
                    Disposer.RemoveAndDispose(ref srv);
                    Disposer.RemoveAndDispose(ref buffer);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BufferViewProxy() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
